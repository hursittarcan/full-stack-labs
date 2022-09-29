import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-logo',
  template: '<div><img src="assets/image.png" alt="logo" height="140px"></div>',
  styles: ['div { background: green; width: 100%; height: 140px;}']
})


export class LogoComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
