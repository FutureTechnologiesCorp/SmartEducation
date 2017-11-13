import { Component, OnInit, Input } from '@angular/core';
import { ControlBase } from "../ControlBase";

@Component({
    selector: 'FieldCheckBox',
    templateUrl: './FieldCheckBox.component.html'
})

export class FieldCheckBox extends ControlBase implements OnInit 
{
    @Input() value: boolean;
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