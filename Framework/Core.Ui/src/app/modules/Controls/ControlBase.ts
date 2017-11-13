import { Input } from '@angular/core';
import { ControlHelper } from './ControlHelper';

export class ControlBase {
    
    @Input() id: string;
    
    constructor() {        
        if(!this.id) 
            this.id = ControlHelper.GetGuid();
    }
}