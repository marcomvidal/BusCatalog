import { Component, Input } from "@angular/core";

@Component({
  selector: 'bus-attention-icon',
  templateUrl: './attention-icon.component.html'
})
export class AttentionIconComponent {
  @Input()
  color = 'gray';
}
