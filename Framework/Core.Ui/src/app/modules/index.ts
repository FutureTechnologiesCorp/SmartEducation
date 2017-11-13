import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
//import { CommonModule } from '@angular/common';
//import { HelloComponent } from './hello/hello.component';
import { FieldText } from './Controls/FieldText/FieldText.component';
import { FieldButton } from "./Controls/FieldButton/FieldButtom.component"
import { FieldCheckBox } from "./Controls/FieldCheckBox/FieldCheckBox.component"
import { FieldDateTime } from "./Controls/FieldDateTime/FieldDateTime.component"
import { FieldDropDown } from "./Controls/FieldDropDown/FieldDropDown.component"
import { SimpleErrorHandler } from './SimpleErrorHandler';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule
  ],
  declarations: [
    FieldText,
    FieldButton,
    FieldCheckBox,
    FieldDateTime,
    FieldDropDown
  ],
  exports: [
    FieldText,
    FieldButton,
    FieldCheckBox,
    FieldDateTime,
    FieldDropDown
  ],
  
  providers: [{ provide: ErrorHandler, useClass: SimpleErrorHandler }]
})
export class CoreUiModules {
}
