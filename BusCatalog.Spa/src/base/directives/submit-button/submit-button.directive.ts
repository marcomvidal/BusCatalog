import { Directive, ElementRef, HostBinding, Input, OnChanges, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[busSubmitButton]'
})
export class SubmitButtonDirective implements OnChanges {
  static readonly BASE_STYLE = 'text-white font-bold py-2 px-4 rounded w-full';

  @HostBinding('class')
  elementClass = SubmitButtonDirective.BASE_STYLE;

  @HostBinding('type')
  type = 'submit';

  @Input()
  isDisabled = false;

  constructor(private element: ElementRef) {
    this.elementClass = this.computedElementClass;
  }

  ngOnChanges(_: SimpleChanges) {
    this.element.nativeElement.disabled = this.isDisabled;
    this.elementClass = this.computedElementClass;
  }

  get computedElementClass() {
    const variation = this.isDisabled
      ? 'bg-gray-500'
      : 'bg-teal-500 hover:bg-teal-700';

    return `${SubmitButtonDirective.BASE_STYLE} ${variation}`;
  }
}
