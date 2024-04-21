import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'searchFilter'
})
export class SearchFilterPipe implements PipeTransform {
  transform(value: any[], searchTerm: string, filterBy: string) {
    return value.length > 0
      ? value.filter(item => item[filterBy]
          .toLowerCase()
          .includes(searchTerm.toLowerCase()))
      : value;
  }
}