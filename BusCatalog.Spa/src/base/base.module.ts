import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeadingComponent } from './components/heading/heading.component';
import { TableComponent } from './components/table/table.component';
import { ButtonDirective } from './directives/button/button.directive';
import { TableCellDirective } from './directives/table-cell/table-cell.directive';
import { TableRowDirective } from './directives/table-row/table-row.directive';
import { InputDirective } from './directives/input/input.directive';
import { LabelDirective } from './directives/label/label.directive';
import { InputWrapperComponent } from './components/input-wrapper/input-wrapper.component';
import { SubmitWrapperDirective } from './directives/submit-wrapper/submit-wrapper.directive';

@NgModule({
  declarations: [
    ButtonDirective,
    HeadingComponent,
    InputDirective,
    InputWrapperComponent,
    LabelDirective,
    SubmitWrapperDirective,
    TableComponent,
    TableCellDirective,
    TableRowDirective,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    ButtonDirective,
    HeadingComponent,
    InputDirective,
    InputWrapperComponent,
    LabelDirective,
    SubmitWrapperDirective,
    TableComponent,
    TableCellDirective,
    TableRowDirective,
  ]
})
export class BaseModule {}
