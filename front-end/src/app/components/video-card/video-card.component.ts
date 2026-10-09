import { Component, input, computed } from '@angular/core';
import { VideoDTO } from '../../interfaces/VideoDTO';

@Component({
  selector: 'app-video-card',
  standalone: true,
  imports: [],
  templateUrl: './video-card.component.html',
  styleUrl: './video-card.component.css',
})
export class VideoCardComponent {
  video = input.required<VideoDTO>();

  readonly formattedDuration = computed(() => {
    const s = this.video().duration;
    const h = Math.floor(s / 3600);
    const m = Math.floor((s % 3600) / 60);
    const sec = s % 60;
    if (h > 0) return `${h}:${String(m).padStart(2, '0')}:${String(sec).padStart(2, '0')}`;
    return `${m}:${String(sec).padStart(2, '0')}`;
  });

  readonly formattedViews = computed(() => {
    const v = this.video().views;
    if (v >= 1_000_000) return `${(v / 1_000_000).toFixed(1).replace(/\.0$/, '')}M views`;
    if (v >= 1_000)     return `${Math.floor(v / 1_000)}K views`;
    return `${v} views`;
  });

  readonly relativeTime = computed(() => {
    const date = new Date(this.video().uploadDate);
    const now = new Date();
    const diffDays = Math.floor((now.getTime() - date.getTime()) / 86_400_000);
    if (diffDays === 0)  return 'Today';
    if (diffDays === 1)  return '1 day ago';
    if (diffDays < 7)   return `${diffDays} days ago`;
    const w = Math.floor(diffDays / 7);
    if (w < 4)          return w === 1 ? '1 week ago' : `${w} weeks ago`;
    const mo = Math.floor(diffDays / 30);
    if (mo < 12)        return mo === 1 ? '1 month ago' : `${mo} months ago`;
    const yr = Math.floor(diffDays / 365);
    return yr === 1 ? '1 year ago' : `${yr} years ago`;
  });

  readonly thumbnailFallback = 'data:image/svg+xml,%3Csvg xmlns%3D%22http%3A//www.w3.org/2000/svg%22 width%3D%22320%22 height%3D%22180%22%3E%3Crect width%3D%22100%25%22 height%3D%22100%25%22 fill%3D%22%23272727%22/%3E%3C/svg%3E';
  readonly avatarFallback = 'data:image/svg+xml,%3Csvg xmlns%3D%22http%3A//www.w3.org/2000/svg%22 width%3D%2236%22 height%3D%2236%22%3E%3Ccircle cx%3D%2218%22 cy%3D%2218%22 r%3D%2218%22 fill%3D%22%23444%22/%3E%3C/svg%3E';

  onThumbnailError(event: Event): void {
    (event.target as HTMLImageElement).src = this.thumbnailFallback;
  }

  onAvatarError(event: Event): void {
    (event.target as HTMLImageElement).src = this.avatarFallback;
  }
}
