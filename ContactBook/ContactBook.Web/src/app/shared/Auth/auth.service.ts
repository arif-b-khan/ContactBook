import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';

import {
    Http
    , Request
    , Response
    , Headers
    , RequestOptions
} from '@angular/http';
import { AppSettings } from '../../app.settings';
import { UserInfo
    , ExternalModel
    , StorageService } from '../index';

@Injectable()
export class AuthService {
    private _isLoggedIn: boolean = false;
    public redirectUrl: string;

    constructor(public _http: Http
        , public _storageSvc: StorageService) {
         this._isLoggedIn = false;
    }

    get IsLoggedIn(): boolean {
        return this._isLoggedIn;
    }

    login(username: string, password: string): Observable<UserInfo> {
        let header = new Headers();
        header.append("Content-Type", "application/x-www-form-urlencoded");
        let data = { Username: username, Password: password, grant_type: "password" };
        let request = new RequestOptions();
        request.headers = header;

        return this._http.post(AppSettings.BASEURL.concat('/Token')
            , JSON.stringify(data)
            , request)
            .map((res: Response) => {
                return <UserInfo>res.json();
            })
            .do((userInfo:UserInfo)=>{
               this._storageSvc.addOrUpdateItem(AppSettings.USERINFOKEY, userInfo);
                this._isLoggedIn = true;
            })
            .catch((err: any, caught: Observable<UserInfo>) => {
                console.log(err);
                return Observable.throw("Login failed");
            });
    }

    logout() :void{
        this._storageSvc.removeItem(AppSettings.USERINFOKEY); //remove the cached url
        this._isLoggedIn = false;
    }

    getExternalLogins():Observable<ExternalModel>
    {
        return this._http
        .get(AppSettings.BASEURL.concat('/api/Account/ExternalLogins?returnUrl=/&generateState=true'))
        .map((res:Response) => <ExternalModel>res.json())
        .catch((err:any, caught: Observable<ExternalModel>) => {
            console.log("unable to load external model");
            return Observable.throw("External login failure");
        });
    }

}