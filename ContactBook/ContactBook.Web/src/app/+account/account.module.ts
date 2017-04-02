import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/index';

//module component
import { AccountComponent } from './index';

@NgModule({
    imports: [SharedModule],
    declarations: [AccountComponent]
})
export class AccountModule {

}