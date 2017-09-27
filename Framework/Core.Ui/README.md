Сделано по инструкции от сюда: 
https://www.tsmean.com/articles/how-to-write-a-typescript-library/angular/

1. для файла ~SmartEducation\Framework\Core.Ui\src\app\app.module.ts в @NgModule импортируем модуль:

import {CoreUiModules} from './modules/index'

@NgModule({  imports: [
    CoreUiModules
  ],
})

2. для файла ~SmartEducation\Framework\Core.Ui\src\app\modules\index.ts импортируем и определяем все вновь добавляемые компоненты

import { HelloComponent } from './hello/hello.component';

@NgModule({
  declarations: [HelloComponent],
  exports: [HelloComponent]
})

Для публикации модулей, необходимо в файле ~SmartEducation\Framework\Core.Ui\src\app\modules\package.json  сменить версию например "version": "0.0.2" => "version": "0.0.3", затем выполнить команду npm publish находясь в 