import { Injectable, signal, computed } from '@angular/core';
import { CategoriesDTO } from '../interfaces/CategoriesDTO';

/**
 * Singleton service that holds the list of categories loaded from the API.
 * Shared between the sidebar, home page, and categories page so the API
 * is only called once per application session.
 */
@Injectable({ providedIn: 'root' })
export class CategoryStateService {
  private readonly _categories = signal<CategoriesDTO[]>([]);
  private readonly _loaded = signal(false);

  /** Read-only signal of the categories list */
  readonly categories = computed(() => this._categories());

  /** Whether categories have already been fetched */
  readonly loaded = computed(() => this._loaded());

  setCategories(cats: CategoriesDTO[]): void {
    this._categories.set(cats);
    this._loaded.set(true);
  }

  findById(id: number): CategoriesDTO | undefined {
    return this._categories().find(c => c.categoryId === id);
  }
}
