import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import { OnChanges } from '@angular/core/src/metadata/lifecycle_hooks';

@Component({
  selector: 'app-loading',
  template: `<div [ngClass]="getClasses()">
                 <mat-spinner aria-label="Loading information" [diameter]="diameter"></mat-spinner>
                 <p *ngIf="message">{{message}}</p>
             </div>`,
  styleUrls: ['./loading.component.scss']
})
export class LoadingComponent implements OnInit {
  @Input() message: string;
  @Input() inline: boolean;
  @Input() size: string;

  // diameter of the spinner; default to xl
  diameter = 75;

  constructor() {}

  /**
   * Handle changes to our Input parameters; specifically monitoring changes to the size one and updating our diameter accordingly.
   */
  ngOnInit(): void {
      switch (this.size) {
        case 'xs':
          this.diameter = 15;
          break;
        case 'sm':
          this.diameter = 20;
          break;
        case 'md':
          this.diameter = 30;
          break;
        case 'lg':
          this.diameter = 50;
          break;
        default:
          this.diameter = 75;
          break;
      }
  }

  getClasses() {
    return {
      'full-spinner': !this.inline,
      'inline-spinner': this.inline
    };
  }
}
