import { Spectator } from "@ngneat/spectator";
import { KeyCode } from "./key-code";
import { DebugElement } from "@angular/core";

export class NgSelectTestHandler<T> {
  constructor(private spectator: Spectator<T>) {}

  openSelect(directiveOrSelector: string | DebugElement) {
    this.spectator.triggerEventHandler(
      directiveOrSelector,
      'keydown',
      { which: KeyCode.Space, preventDefault: () => { } });
  }

  selectOption(directiveOrSelector: string | DebugElement) {
    this.spectator.triggerEventHandler(
      directiveOrSelector,
      'keydown',
      { which: KeyCode.Enter, preventDefault: () => { } });
  }

  toggleNextOption(directiveOrSelector: string | DebugElement) {
    this.spectator.triggerEventHandler(
      directiveOrSelector,
      'keydown',
      { which: KeyCode.ArrowDown, preventDefault: () => { } });
  }
}
