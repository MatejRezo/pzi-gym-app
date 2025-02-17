import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, switchMap, take } from 'rxjs';
import { getCurrentEnv } from 'src/app/constants/environments';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  apiUrl = getCurrentEnv();
  currentUser: any = {};

  constructor(private http: HttpClient) { }

  getNews() {
    return this.http.get(`${this.apiUrl}/news`)
      .pipe(take(1));
  }

  addNews(newsItem: any) {
    return this.http.post(`${this.apiUrl}/news`, newsItem)
      .pipe(take(1));
  }

  deleteNews(id: number) {
    return this.http.delete(`${this.apiUrl}/news/${id}`)
      .pipe(take(1));
  }

  updateNews(news: any) {
    return this.http.put(`${this.apiUrl}/news`, news)
      .pipe(take(1));
  }

  getPersonalDetails() {
    const subj = new Subject();

    this.http.get(`${this.apiUrl}/users/me`)
      .pipe(take(1))
      .subscribe((res: any) => {
        this.currentUser = res;
        subj.next(res);
      });
    
    return subj.asObservable();
  }
}
