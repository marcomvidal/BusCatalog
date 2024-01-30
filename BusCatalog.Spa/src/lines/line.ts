export class Line {
  constructor(
    public id: number,
    public identification: string,
    public fromwards: string,
    public towards: string,
    public departuresPerDay: number) {}
}