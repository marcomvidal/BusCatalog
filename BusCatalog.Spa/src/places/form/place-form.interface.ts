import { FormControl } from "@angular/forms";

export interface PlaceForm {
  identification: FormControl<string>,
  city: FormControl<string>
}
