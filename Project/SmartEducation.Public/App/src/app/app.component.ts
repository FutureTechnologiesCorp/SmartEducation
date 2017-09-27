import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http'
import {HelloComponent} from 'core.ui/hello/hello.component'

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
    constructor(private _httpService: Http) { }
    apiValues: string[] = [];

    obj: HelloComponent = new HelloComponent();

    ngOnInit() {
        this._httpService.get('/api/Start').subscribe(values => {
            this.apiValues = values.json() as string[];
        });
    }
}
