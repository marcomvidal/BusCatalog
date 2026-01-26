import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: 'searchFilter'
})
export class SearchFilterPipe implements PipeTransform {
  transform(data: any[], searchTerm: string, filterBy: string[]) {
    return data.length > 0
      ? data.filter(item =>
          filterBy.some(attribute => 
            item[attribute]
              .toLowerCase()
              .includes(searchTerm.toLowerCase())))
      : data;
  }
}