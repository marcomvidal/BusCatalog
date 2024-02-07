import { Component, Input } from '@angular/core';

@Component({
  selector: 'bus-heading',
  templateUrl: './heading.component.html',
  styleUrl: './heading.css'
})
export class HeadingComponent {
  @Input()
  title = '';

  @Input()
  subTitle = '';
}
