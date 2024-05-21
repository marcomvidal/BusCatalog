import { FormGroup } from "@angular/forms";
import { LineForm } from "./form/line-form.interface";

export class Line {
  constructor(
    public identification: string,
    public fromwards: string,
    public towards: string,
    public departuresPerDay: number,
    public vehicles: string[],
    public id?: number) {}

    static fromForm(form: FormGroup<LineForm>) {
      return new Line(
        form.get('identification')?.value!,
        form.get('fromwards')?.value!,
        form.get('towards')?.value!,
        form.get('departuresPerDay')?.value!,
        form.get('vehicles')?.value!);
    }
}