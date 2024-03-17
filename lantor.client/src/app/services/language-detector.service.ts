import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { LanguageDetectorRequest } from '../model/language-detector-request';
import { LanguageSimilarityResult, LanguageSimilarityValue } from '../model/language-similarity-result';

@Injectable({
  providedIn: 'root'
})
export class LanguageDetectorService {

  constructor(private http: HttpClient) {
  }

  calculateSimilarityValues(text: string): Observable<LanguageSimilarityResult> {
    let body = new LanguageDetectorRequest(text);
    return this.http.post<LanguageSimilarityResult>('/api/detectlanguages', body);
  }
}
