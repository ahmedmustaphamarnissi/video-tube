import { VideoDTO } from './VideoDTO';

export interface VideoDetailsDTO extends VideoDTO {
  likesCount: number;
  dislikesCount: number;
  description: string | null;
  videoUrl: string | null;
}