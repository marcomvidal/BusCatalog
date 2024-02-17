import { FormControl } from "@angular/forms";

export interface VehicleForm {
  identification: FormControl<string>,
  description: FormControl<string>
}
