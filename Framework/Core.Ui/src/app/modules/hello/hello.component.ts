import { Component, OnInit, Input } from '@angular/core';
//import { Injectable } from '@angular/core';

@Component({
  selector: 'app-hello',
  template: 
  `<p>
    <strong>
      hello {{rabotaysuka}}!
    </strong>
  </p>`,
  styleUrls: ['./hello.component.css']
})
export class HelloComponent implements OnInit {
  @Input() rabotaysuka:string;

  constructor() {}

  ngOnInit() {
  }

}
