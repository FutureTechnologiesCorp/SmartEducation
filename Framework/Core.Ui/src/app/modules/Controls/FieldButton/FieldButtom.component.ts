import { Component, OnInit, Input } from '@angular/core';
import { ControlBase } from "../ControlBase";

@Component({
    selector: 'FieldButton',
    templateUrl: './FieldButton.component.html'
})

export class FieldButton extends ControlBase implements OnInit 
{
    @Input() text: string;

    constructor() {
        super();
    }

    ngOnInit(): void { throw new Error("Method not implemented."); }

    onClickBtn(event, btn : FieldButton) {        
        event.preventDefault();
        alert("clicked on btn " + btn.text);
    }
}