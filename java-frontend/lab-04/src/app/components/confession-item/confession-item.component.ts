// @ts-ignore
import { Component, Input, OnInit } from '@angular/core';
import { Confession } from 'src/app/models/confession.model';

// @ts-ignore
@Component({
  selector: 'app-confession-item',
  templateUrl: './confession-item.component.html',
  styleUrls: ['./confession-item.component.css']
})

export class ConfessionItemComponent implements OnInit {
  @Input() confession!: Confession;
  constructor() { }

  ngOnInit(): void {
  }

  addLike(): void {
    this.confession.likes++;
  }

  addDislike(): void {
    this.confession.dislikes++;
  }
}
