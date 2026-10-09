import { Component, OnInit, inject, signal } from '@angular/core';
import { DataService } from '../../services/data-service';
import { VideoDTO } from '../../interfaces/VideoDTO';
import { VideoCardComponent } from '../../components/video-card/video-card.component';

type LoadState = 'loading' | 'success' | 'error' | 'empty';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [VideoCardComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  private readonly dataService = inject(DataService);

  readonly videos = signal<VideoDTO[]>([]);
  readonly loadState = signal<LoadState>('loading');

  private currentPage = 1;
  private readonly pageSize = 20;

  /** Whether there might be more pages (optimistic: true until API returns < pageSize) */
  readonly hasMore = signal(true);
  readonly loadingMore = signal(false);

  ngOnInit(): void {
    this.loadVideos();
  }

  private loadVideos(): void {
    this.loadState.set('loading');
    this.dataService.GetHomeVideosAsync(this.currentPage, this.pageSize).subscribe({
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

    this.dataService.GetHomeVideosAsync(this.currentPage, this.pageSize).subscribe({
      next: (data: VideoDTO[]) => {
        this.videos.update(prev => [...prev, ...data]);
        this.hasMore.set(data.length === this.pageSize);
        this.loadingMore.set(false);
      },
      error: () => {
        this.currentPage--;   // roll back
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
