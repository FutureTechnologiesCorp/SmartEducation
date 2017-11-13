import { Component, OnInit, Input } from '@angular/core';
import { ControlBase } from "../ControlBase";

@Component({
    selector: 'FieldDateTime',
    templateUrl: './FieldDateTime.component.html'
})

export class FieldDateTime extends ControlBase implements OnInit 
{
    @Input() value: Date;
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