import { FormControl } from "@angular/forms";

export interface LineForm {
  identification: FormControl<string>,
  fromwards: FormControl<string>,
  towards: FormControl<string>,
  departuresPerDay: FormControl<number>,
  vehicles: FormControl<string[]>
}
