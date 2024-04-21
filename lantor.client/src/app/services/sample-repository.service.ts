import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AlphabetListInfo } from '../model/alphabet-list-info';
import { EmptyMultilingualSample } from '../model/empty-multilingual-sample';
import { LanguageSample } from '../model/language-sample';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';

@Injectable({
  providedIn: 'root'
})
export class SampleRepositoryService {

  constructor(private http: HttpClient) { }

  getMultilingualSamples(): Observable<MultilingualSampleListInfo[]> {
    return this.http.get<MultilingualSampleListInfo[]>('/api/multilingualsample');
  }

  getAlphabets(): Observable<AlphabetListInfo[]> {
    let fakeData = [
      new AlphabetListInfo(1, "Default", 500),
      new AlphabetListInfo(2, "Medium", 5000),
      new AlphabetListInfo(3, "Large", 10000)
    ];
    return of(fakeData);
  }

  getMultilingualSample(id: number): Observable<EmptyMultilingualSample | null> {
    return this.http.get<EmptyMultilingualSample>(`/api/multilingualsample/${id}`);
  }

  getLanguageSample(id: number): Observable<LanguageSample | null> {
    return this.http.get<LanguageSample>(`/api/languagesample/${id}`);
  }

  updateMultilingualSample(mls: EmptyMultilingualSample): Observable<void> {
    return this.http.put<void>("/api/multilingualsample", mls);
  }

}
