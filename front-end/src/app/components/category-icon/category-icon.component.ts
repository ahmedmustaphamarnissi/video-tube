import { Component, input } from '@angular/core';

/**
 * Renders a safe, inline SVG icon based on the iconName from the API.
 * All SVG paths are hardcoded here - no dynamic HTML injection.
 */
@Component({
  selector: 'app-category-icon',
  standalone: true,
  imports: [],
  templateUrl: './category-icon.component.html',
  styleUrl: './category-icon.component.css',
})
export class CategoryIconComponent {
  iconName = input<string | null>(null);
  size = input<number>(20);
}
