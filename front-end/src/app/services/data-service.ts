import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VideoDTO } from '../interfaces/VideoDTO';
import { VideoDetailsDTO } from '../interfaces/VideoDetailsDTO';
import { CommentDTO } from '../interfaces/CommentDTO';
import { CategoriesDTO } from '../interfaces/CategoriesDTO';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private http: HttpClient) {}

  GetHomeVideosAsync(page: number, items: number): Observable<VideoDTO[]> {
    const params = new HttpParams().set('pageNumber', page).set('pageSize', items);
    return this.http.get<VideoDTO[]>(`https://localhost:7028/api/videos`, { params });
  }

  GetVideosByCategoryAsync(id: number, page: number, items: number): Observable<VideoDTO[]> {
    const params = new HttpParams().set('categoryId', id).set('pageNumber', page).set('pageSize', items);
    return this.http.get<VideoDTO[]>(`http://localhost:7028/api/videos/category/${id}`, { params });
  }

  GetVideoDetailsAsync(id: number): Observable<VideoDetailsDTO> {
    const params = new HttpParams().set('id', id);
    return this.http.get<VideoDetailsDTO>(`https://localhost:7028/api/videos/${id}`, { params });
  }

  GetCommentsByVideoIdAsync(id: number): Observable<CommentDTO[]> {
    const params = new HttpParams().set('videoId', id);
    return this.http.get<CommentDTO[]>(`https://localhost:7028/api/comments/${id}`, { params });
  }

  GetCategoriesAsync(): Observable<CategoriesDTO[]> {
    return this.http.get<CategoriesDTO[]>(`https://localhost:7028/api/categories`);
  }
}
