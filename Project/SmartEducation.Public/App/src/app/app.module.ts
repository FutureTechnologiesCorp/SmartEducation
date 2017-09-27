import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { SampleModule } from 'angularcontrolslibrary'

import { CoreUiModules } from 'core.ui'

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        SampleModule,
        CoreUiModules
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
