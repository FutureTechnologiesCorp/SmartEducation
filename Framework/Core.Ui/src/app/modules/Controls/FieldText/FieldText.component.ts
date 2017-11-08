import { Component, OnInit, Input } from '@angular/core';
import { ControlHelper } from '../ControlHelper';

@Component({
    selector: 'FieldText',
    templateUrl: './FieldText.component.html',
})

export class FieldText implements OnInit 
{
    @Input() label: string;
    @Input() value: string;
    @Input() id: string;

    constructor() {
        if(!this.id) 
            this.id = ControlHelper.GetGuid();
    }


    ngOnInit(): void { throw new Error("Method not implemented."); }
}