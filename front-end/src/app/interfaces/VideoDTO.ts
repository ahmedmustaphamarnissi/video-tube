export interface VideoDTO {
  videoId: number;
  title: string;
  duration: number;
  uploadDate: string;
  views: number;
  thumbnailUrl: string;
  channelName: string;
  channelProfileUrl: string;
  isVerified: boolean;
}