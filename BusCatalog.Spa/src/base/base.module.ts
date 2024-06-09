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
import { FormFieldsWrapperDirective } from './directives/submit-wrapper/form-fields-wrapper.directive';
import { SubmitButtonDirective } from './directives/submit-button/submit-button.directive';
import { VehicleIconComponent } from './icons/vehicle/vehicle-icon.component';
import { LineIconComponent } from './icons/line/line-icon.component';
import { BackEndAlertComponent } from './components/back-end-alert/back-end.alert.component';
import { AttentionIconComponent } from './icons/attention/attention-icon.component';
import { SpinnerComponent } from './icons/spinner/spinner.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SearchInputComponent } from './components/search-input/search-input.component';
import { WarningButtonDirective } from './directives/warning-button/warning-button.directive';
import { SearchFilterPipe } from './pipes/search-filter/search-filter.pipe';

@NgModule({
  declarations: [
    AttentionIconComponent,
    BackEndAlertComponent,
    ButtonDirective,
    HeadingComponent,
    InputDirective,
    InputWrapperComponent,
    LabelDirective,
    LineIconComponent,
    FormFieldsWrapperDirective,
    VehicleIconComponent,
    SearchFilterPipe,
    SearchInputComponent,
    SpinnerComponent,
    SubmitButtonDirective,
    TableComponent,
    TableCellDirective,
    TableRowDirective,
    WarningButtonDirective
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    AttentionIconComponent,
    BackEndAlertComponent,
    ButtonDirective,
    HeadingComponent,
    InputDirective,
    InputWrapperComponent,
    LabelDirective,
    LineIconComponent,
    FormFieldsWrapperDirective,
    VehicleIconComponent,
    SearchFilterPipe,
    SearchInputComponent,
    SpinnerComponent,
    SubmitButtonDirective,
    TableComponent,
    TableCellDirective,
    TableRowDirective,
    WarningButtonDirective
  ]
})
export class BaseModule {}
