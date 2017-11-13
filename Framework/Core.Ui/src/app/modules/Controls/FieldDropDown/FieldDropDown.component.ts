import { Component, OnInit, Input } from '@angular/core';
import { ControlBase } from "../ControlBase";

@Component({
    selector: 'FieldDropDown',
    templateUrl: './FieldDropDown.component.html'
})

export class FieldDropDown extends ControlBase implements OnInit 
{
    @Input() value: number;
    @Input() label: string;
    
    public canShow = false;

    public countries = [
        {id: 1, name: "Москва"},
        {id: 2, name: "Питербург"},
        {id: 3, name: "Новосибирск"},
        {id: 4, name: "Калининград"},
        {id: 5, name: "Липецк"}
      ];
    public selectedValue = null;

    constructor() {
        super();
    }


    ngOnInit(): void {
        if(this.label){
            this.canShow = true;
        }

        if(this.value > 0)
        {
            this.selectedValue = this.value;
        }
    }
}