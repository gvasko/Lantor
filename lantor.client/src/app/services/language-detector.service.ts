import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LanguageDetectorRequest } from '../model/language-detector-request';
import { LanguageSimilarityResult } from '../model/language-similarity-result';

@Injectable({
  providedIn: 'root'
})
export class LanguageDetectorService {

  constructor(private http: HttpClient) {
  }

  calculateDefaultSimilarityValues(text: string): Observable<LanguageSimilarityResult> {
    let body = new LanguageDetectorRequest(text);
    return this.http.post<LanguageSimilarityResult>('/api/DetectLanguages/Default', body);
  }

  calculateCustomSimilarityValues(text: string, sampleId: number, alphabetId: number): Observable<LanguageSimilarityResult> {
    let body = new LanguageDetectorRequest(text, sampleId, alphabetId);
    return this.http.post<LanguageSimilarityResult>('/api/DetectLanguages/Custom', body);
  }

}
