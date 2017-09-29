import { NgModule, ErrorHandler } from '@angular/core';

export class SimpleErrorHandler implements ErrorHandler {
  handleError(error) {
    //здесь по идее нужно перекинуть на страницу с ошибкой при ошибке в реализации
    // alert(error);
  }
}