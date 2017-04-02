import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService, AuthGaurd
,StorageService
 } from './index';

@NgModule({
    exports: [CommonModule, FormsModule, AuthService, AuthGaurd],
    providers:[StorageService]
})
export class SharedModule{

}