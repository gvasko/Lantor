import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AlphabetListInfo } from '../model/alphabet-list-info';
import { LanguageDetectorRequest } from '../model/language-detector-request';
import { LanguageSimilarityResult, LanguageSimilarityValue } from '../model/language-similarity-result';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';

@Injectable({
  providedIn: 'root'
})
export class LanguageDetectorService {

  constructor(private http: HttpClient) {
  }

  calculateDefaultSimilarityValues(text: string): Observable<LanguageSimilarityResult> {
    let body = new LanguageDetectorRequest(text);
    return this.http.post<LanguageSimilarityResult>('/api/detectlanguages', body);
  }

  calculateCustomSimilarityValues(text: string, sampleId: number, alphabetId: number): Observable<LanguageSimilarityResult> {
    let body = new LanguageDetectorRequest(text, sampleId, alphabetId);
    return this.http.post<LanguageSimilarityResult>('/api/detectlanguages', body);
  }

  getMultilingualSamples(): Observable<MultilingualSampleListInfo[]> {
    let fakeData = [
      new MultilingualSampleListInfo(1, "Default", 10),
      new MultilingualSampleListInfo(2, "Custom 1", 15),
      new MultilingualSampleListInfo(3, "Custom 2", 20)
    ];
    return of(fakeData);
  }

  getAlphabets(): Observable<AlphabetListInfo[]> {
    let fakeData = [
      new AlphabetListInfo(1, "Default", 500),
      new AlphabetListInfo(2, "Medium", 5000),
      new AlphabetListInfo(3, "Large", 10000)
    ];
    return of(fakeData);
  }
}
