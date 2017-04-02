import { Injectable } from '@angular/core';

@Injectable()
export class StorageService {
    
    addOrUpdateItem<T>(key: string, t: T): void {
        this.removeItem(key);
        sessionStorage.setItem(key, JSON.stringify(t));
    }
    
    removeItem(key:string):boolean{
       if(this.contains(key)){
           sessionStorage.removeItem(key);
           return true;
       }
       return false;
    }

    getItem<T>(key: string): T {
        let data: T = null;
        if (this.contains(key)) {
            data = <T>JSON.parse(sessionStorage.getItem(key), (key: string, value: any) => {
                 if(key.endsWith("Date")){
                     return new Date(value);
                 }
                 return value;
            });
        }
        return data;
    }

    contains(key:string): boolean{
        if (key === undefined || key === null) {
            throw { message: "argument cannot be undefined or null" };
        }

        if(sessionStorage.length <= 0){
            return false;
        }

        let objStr:any = sessionStorage.getItem(key);
        if(objStr !== undefined || objStr !== null){
            return true;
        }
        return false;
    }


    clearAll():void{
        sessionStorage.clear();
    }
}