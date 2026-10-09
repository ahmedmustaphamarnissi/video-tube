import { Component, OnInit, OnDestroy, inject, signal, computed } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { DataService } from '../../services/data-service';
import { VideoDetailsDTO } from '../../interfaces/VideoDetailsDTO';
import { CommentDTO } from '../../interfaces/CommentDTO';

type LoadState = 'loading' | 'success' | 'error';

@Component({
  selector: 'app-video-details',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './video-details.component.html',
  styleUrl: './video-details.component.css',
})
export class VideoDetailsComponent implements OnInit, OnDestroy {
  private readonly route = inject(ActivatedRoute);
  private readonly dataService = inject(DataService);

  readonly video = signal<VideoDetailsDTO | null>(null);
  readonly comments = signal<CommentDTO[]>([]);
  readonly loadState = signal<LoadState>('loading');
  readonly commentsState = signal<'loading' | 'success' | 'error'>('loading');
  readonly showFullDescription = signal(false);

  private routeSub?: Subscription;
  private delayTimer?: ReturnType<typeof setTimeout>;

  readonly formattedDuration = computed(() => {
    const s = this.video()?.duration ?? 0;
    const h = Math.floor(s / 3600);
    const m = Math.floor((s % 3600) / 60);
    const sec = s % 60;
    if (h > 0) return `${h}:${String(m).padStart(2, '0')}:${String(sec).padStart(2, '0')}`;
    return `${m}:${String(sec).padStart(2, '0')}`;
  });

  readonly formattedViews = computed(() => {
    const v = this.video()?.views ?? 0;
    if (v >= 1_000_000) return `${(v / 1_000_000).toFixed(1).replace(/\.0$/, '')}M views`;
    if (v >= 1_000) return `${Math.floor(v / 1_000)}K views`;
    return `${v} views`;
  });

  readonly formattedLikes = computed(() => {
    const v = this.video()?.likesCount ?? 0;
    if (v >= 1_000_000) return `${(v / 1_000_000).toFixed(1).replace(/\.0$/, '')}M`;
    if (v >= 1_000) return `${(v / 1_000).toFixed(1).replace(/\.0$/, '')}K`;
    return `${v}`;
  });

  readonly formattedDate = computed(() => {
    const d = this.video()?.uploadDate;
    if (!d) return '';
    return new Date(d).toLocaleDateString('en-US', {
      year: 'numeric', month: 'long', day: 'numeric',
    });
  });

  ngOnInit(): void {
    this.routeSub = this.route.paramMap.subscribe(params => {
      const idStr = params.get('id');
      const id = idStr ? parseInt(idStr, 10) : NaN;
      if (isNaN(id)) { this.loadState.set('error'); return; }
      this.load(id);
    });
  }

  ngOnDestroy(): void {
    this.routeSub?.unsubscribe();
    if (this.delayTimer) clearTimeout(this.delayTimer);
  }

  private load(id: number): void {
    this.loadState.set('loading');
    this.commentsState.set('loading');
    this.video.set(null);
    this.comments.set([]);
    this.showFullDescription.set(false);
    if (this.delayTimer) clearTimeout(this.delayTimer);

    this.dataService.GetVideoDetailsAsync(id).subscribe({
      next: (data) => {
        this.video.set(data);
        this.loadState.set('success');
      },
      error: () => {
        this.delayTimer = setTimeout(() => this.loadState.set('error'), 5000);
      },
    });

    this.dataService.GetCommentsByVideoIdAsync(id).subscribe({
      next: (data) => {
        this.comments.set(data);
        this.commentsState.set('success');
      },
      error: () => this.commentsState.set('error'),
    });
  }

  formatCommentDate(dateStr: string): string {
    const date = new Date(dateStr);
    const now = new Date();
    const diffDays = Math.floor((now.getTime() - date.getTime()) / 86_400_000);
    if (diffDays === 0) return 'Today';
    if (diffDays === 1) return '1 day ago';
    if (diffDays < 7) return `${diffDays} days ago`;
    const w = Math.floor(diffDays / 7);
    if (w < 4) return w === 1 ? '1 week ago' : `${w} weeks ago`;
    const mo = Math.floor(diffDays / 30);
    if (mo < 12) return mo === 1 ? '1 month ago' : `${mo} months ago`;
    const yr = Math.floor(diffDays / 365);
    return yr === 1 ? '1 year ago' : `${yr} years ago`;
  }

  formatCommentLikes(n: number): string {
    if (n >= 1_000) return `${(n / 1_000).toFixed(1).replace(/\.0$/, '')}K`;
    return `${n}`;
  }

  readonly thumbnailFallback = 'data:image/svg+xml,%3Csvg xmlns%3D%22http%3A//www.w3.org/2000/svg%22 width%3D%22320%22 height%3D%22180%22%3E%3Crect width%3D%22100%25%22 height%3D%22100%25%22 fill%3D%22%23272727%22/%3E%3C/svg%3E';
  readonly avatarFallback = 'data:image/svg+xml,%3Csvg xmlns%3D%22http%3A//www.w3.org/2000/svg%22 width%3D%2236%22 height%3D%2236%22%3E%3Ccircle cx%3D%2218%22 cy%3D%2218%22 r%3D%2218%22 fill%3D%22%23444%22/%3E%3C/svg%3E';

  onImgError(event: Event, fallback: string): void {
    (event.target as HTMLImageElement).src = fallback;
  }
}
