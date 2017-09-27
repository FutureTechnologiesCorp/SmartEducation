import { Component  } from '@angular/core';
import{HelloComponent} from './modules/hello/hello.component'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  constructor(){
  }
  
}
