import { Component } from '@angular/core';
import { LINKS } from './links';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  links = LINKS;
}
