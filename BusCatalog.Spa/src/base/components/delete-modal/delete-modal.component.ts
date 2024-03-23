import { Component, Input } from '@angular/core';

@Component({
  selector: 'bus-delete-modal',
  templateUrl: './delete-modal.component.html'
})
export class DeleteModalComponent {
  @Input()
  title = '';

  @Input()
  onSelectedYes: any;

  onYes(answer: boolean) {
    console.log('Selected [yes]');
  }

  onNo() {
    console.log('Selected [no]');
  }
}
