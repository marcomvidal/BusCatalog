import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Link } from './link';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  @Input()
  title: string = '';

  links: Link[] = [
    { label: 'Lines', url: '/' },
    { label: 'Vehicles', url: '/vehicles' }
  ];
}
