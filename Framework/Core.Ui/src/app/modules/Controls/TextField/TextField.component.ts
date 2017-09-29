import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'TextField',
    template: `<input id="{{Id}}" type="text" value="{{value}}" />`,
})

export class TextField implements OnInit {
    @Input() value: string;

    constructor() { };

    ngOnInit(): void { throw new Error("Method not implemented."); }
}

