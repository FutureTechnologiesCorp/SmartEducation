import { NgModule, ErrorHandler } from '@angular/core';
//import { CommonModule } from '@angular/common';
//import { HelloComponent } from './hello/hello.component';
import { FieldText } from './Controls/FieldText/FieldText.component';
import { SimpleErrorHandler } from './SimpleErrorHandler';

@NgModule({
  /*imports: [
    CommonModule
  ],*/
  declarations: [
    FieldText
  ],
  exports: [
    FieldText
  ],
  providers: [{ provide: ErrorHandler, useClass: SimpleErrorHandler }]
})
export class CoreUiModules {
}
