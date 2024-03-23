import { FormGroup } from "@angular/forms";
import { PlaceForm } from "./form/place-form.interface";

export class Place {
  constructor(
    public identification: string = '',
    public city: string = '',
    public id?: number) {}

  static fromRequest(place: Place) {
    return new Place(
      place.identification,
      place.city,
      place.id);
  }

  static fromForm(form: FormGroup<PlaceForm>) {
    return new Place(
      form.get('identification')?.value,
      form.get('city')?.value);
  }
}
