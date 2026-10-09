import { Component, output } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  /** Emitted when the hamburger menu button is clicked */
  menuToggle = output<void>();

  onMenuClick(): void {
    this.menuToggle.emit();
  }
}
