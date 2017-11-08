import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { CoreUiModules } from './modules/index'

@NgModule({
  declarations: [ 
    AppComponent
  ],
  imports: [
    BrowserModule,
    CoreUiModules
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 
  
}
