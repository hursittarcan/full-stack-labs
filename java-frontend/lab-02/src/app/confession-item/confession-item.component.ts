import { Component, OnInit } from '@angular/core';
import {Confession} from "../models/confession.model";

@Component({
  selector: 'app-confession-item',
  templateUrl: './confession-item.component.html',
  styleUrls: ['./confession-item.component.css']
})
export class ConfessionItemComponent implements OnInit {
  confession: Confession | undefined;

  constructor() { }

  ngOnInit(): void {
    this.confession = new Confession("onze eerste confession", "PXL-Digital","Dries");
  }

}
