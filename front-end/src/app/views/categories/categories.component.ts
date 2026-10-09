import { Component, OnInit, OnDestroy, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { DataService } from '../../services/data-service';
import { CategoryStateService } from '../../services/category-state.service';
import { VideoDTO } from '../../interfaces/VideoDTO';
import { CategoriesDTO } from '../../interfaces/CategoriesDTO';
import { VideoCardComponent } from '../../components/video-card/video-card.component';
import { CategoryIconComponent } from '../../components/category-icon/category-icon.component';

type LoadState = 'loading' | 'success' | 'error' | 'empty' | 'invalid';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [VideoCardComponent, CategoryIconComponent],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.css',
})
export class CategoriesComponent implements OnInit, OnDestroy {
  private readonly route = inject(ActivatedRoute);
  private readonly dataService = inject(DataService);
  private readonly categoryState = inject(CategoryStateService);

  readonly videos = signal<VideoDTO[]>([]);
  readonly loadState = signal<LoadState>('loading');
  readonly currentCategory = signal<CategoriesDTO | null>(null);

  readonly hasMore = signal(true);
  readonly loadingMore = signal(false);

  private currentPage = 1;
  private readonly pageSize = 20;
  private routeSub?: Subscription;
  private currentCategoryId = 0;

  ngOnInit(): void {
    this.routeSub = this.route.paramMap.subscribe(params => {
      const idStr = params.get('id');
      const id = idStr ? parseInt(idStr, 10) : NaN;

      if (isNaN(id)) {
        this.loadState.set('invalid');
        return;
      }

      // Reset state when route changes
      this.currentPage = 1;
      this.currentCategoryId = id;
      this.videos.set([]);
      this.hasMore.set(true);

      // Resolve category metadata
      const cat = this.categoryState.findById(id);
      this.currentCategory.set(cat ?? null);

      this.loadVideos();
    });
  }

  ngOnDestroy(): void {
    this.routeSub?.unsubscribe();
  }

  private loadVideos(): void {
    this.loadState.set('loading');
    this.dataService
      .GetVideosByCategoryAsync(this.currentCategoryId, this.currentPage, this.pageSize)
      .subscribe({
        next: (data: VideoDTO[]) => {
          this.videos.set(data);
          this.loadState.set(data.length === 0 ? 'empty' : 'success');
          this.hasMore.set(data.length === this.pageSize);
        },
        error: () => {
          this.loadState.set('error');
        },
      });
  }

  loadMore(): void {
    if (this.loadingMore()) return;
    this.loadingMore.set(true);
    this.currentPage++;

    this.dataService
      .GetVideosByCategoryAsync(this.currentCategoryId, this.currentPage, this.pageSize)
      .subscribe({
        next: (data: VideoDTO[]) => {
          this.videos.update(prev => [...prev, ...data]);
          this.hasMore.set(data.length === this.pageSize);
          this.loadingMore.set(false);
        },
        error: () => {
          this.currentPage--;
          this.loadingMore.set(false);
        },
      });
  }

  retry(): void {
    this.currentPage = 1;
    this.loadVideos();
  }

  trackByVideoId(_index: number, v: VideoDTO): number {
    return v.videoId;
  }
}
