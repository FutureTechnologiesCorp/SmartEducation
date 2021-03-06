import { Component, OnInit, Input } from '@angular/core';
import { ControlBase } from "../ControlBase";

@Component({
    selector: 'FieldText',
    templateUrl: './FieldText.component.html',
})

export class FieldText extends ControlBase implements OnInit 
{
    @Input() value: string;
    @Input() label: string;
    public canShow = false;
    constructor() {
        super();
    }


    ngOnInit(): void {
        if(this.label){
            this.canShow = true;
        }
    }
}