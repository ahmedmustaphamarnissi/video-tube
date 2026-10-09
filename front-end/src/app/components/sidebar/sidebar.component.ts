import { Component, OnInit, inject, input } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { DataService } from '../../services/data-service';
import { CategoryStateService } from '../../services/category-state.service';
import { CategoryIconComponent } from '../category-icon/category-icon.component';
import { CategoriesDTO } from '../../interfaces/CategoriesDTO';

interface SidebarItem {
  label: string;
  iconName: string;
  route?: string;
  isCategory?: boolean;
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CategoryIconComponent],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css',
})
export class SidebarComponent implements OnInit {
  /** When true the sidebar is visible (controlled by parent on mobile) */
  isOpen = input(true);

  private readonly dataService = inject(DataService);
  readonly categoryState = inject(CategoryStateService);

  /** Static sidebar items that are visual-only (no real route) */
  readonly staticTopItems: SidebarItem[] = [
    { label: 'Home',     iconName: 'home', route: '/' },
    { label: 'Trending', iconName: 'trending' },
    { label: 'Films',    iconName: 'films' },
    { label: 'Live',     iconName: 'live' },
  ];

  readonly staticBottomItems: SidebarItem[] = [
    { label: 'Learning',       iconName: 'learning' },
    { label: 'Fashion & Beauty', iconName: 'fashion' },
  ];

  readonly settingsItems: SidebarItem[] = [
    { label: 'Settings',       iconName: 'settings' },
    { label: 'Report History', iconName: 'history' },
    { label: 'Help',           iconName: 'help' },
    { label: 'Send Feedback',  iconName: 'feedback' },
  ];

  ngOnInit(): void {
    // Load categories only once globally
    if (!this.categoryState.loaded()) {
      this.dataService.GetCategoriesAsync().subscribe({
        next: (cats: CategoriesDTO[]) => this.categoryState.setCategories(cats),
        error: (err: unknown) => console.error('Failed to load categories:', err),
      });
    }
  }

  trackByCategoryId(_index: number, cat: CategoriesDTO): number {
    return cat.categoryId;
  }
}
