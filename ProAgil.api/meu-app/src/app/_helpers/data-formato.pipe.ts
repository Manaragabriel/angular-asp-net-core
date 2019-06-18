import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
  name: 'dataFormato'
})
export class DataFormatoPipe extends DatePipe  implements PipeTransform {

  transform(value: any, args?: any): any {
    return super.transform(value,'dd/MM/yyyy hh:mm')
    }

}
