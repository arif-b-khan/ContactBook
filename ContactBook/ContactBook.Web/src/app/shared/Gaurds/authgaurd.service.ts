import { Injectable } from '@angular/core';
import {
    CanActivate, Router,
    ActivatedRouteSnapshot,
    RouterStateSnapshot,
    CanActivateChild,
    NavigationExtras,
    CanLoad, Route
} from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { AuthService } from '../index';


@Injectable()
export class AuthGaurd implements CanActivate, CanActivateChild, CanLoad {
    constructor(private _auth: AuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let url = state.url;
        return this.checkLogin(url);
    }

    canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
        return Observable.of(this.canActivate(childRoute,state));
    }

    canLoad(route: Route): Observable<boolean> {
        let url = `/${route.path}`;
        return Observable.of(this.checkLogin(url));
    }

    private checkLogin(url: string): boolean {
        if (this._auth.IsLoggedIn){
            return true;
        }
        this._auth.redirectUrl = url;
        let session_id = 123456789;

        //create dummy session id
        let navigationExtras:NavigationExtras = <NavigationExtras>{
            queryParams: {'sesion_id': session_id},
            fragment: 'anchor'
        };
        this.router.navigate(['/login'], navigationExtras);
        return false;
    }

}