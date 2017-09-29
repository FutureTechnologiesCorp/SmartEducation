import { NgModule, ErrorHandler } from '@angular/core';
//import { CommonModule } from '@angular/common';
//import { HelloComponent } from './hello/hello.component';
import { TextField } from './Controls/TextField/TextField.component';
import { SimpleErrorHandler } from './SimpleErrorHandler';

@NgModule({
  /*imports: [
    CommonModule
  ],*/
  declarations: [
    TextField
  ],
  exports: [
    TextField
  ],
  providers: [{ provide: ErrorHandler, useClass: SimpleErrorHandler }]
})
export class CoreUiModules {
}
