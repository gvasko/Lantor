import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { LanguageSimilarityResult, LanguageSimilarityValue } from '../model/language-similarity-result';

@Injectable({
  providedIn: 'root'
})
export class LanguageDetectorService {

  constructor(private http: HttpClient) {
  }

  calculateSimilarityValues(text: string): Observable<LanguageSimilarityResult> {
    let result = new LanguageSimilarityResult();
    result.similarityValues.push(new LanguageSimilarityValue("en", 0.15));
    result.similarityValues.push(new LanguageSimilarityValue("de", 0.11));
    result.similarityValues.push(new LanguageSimilarityValue("hu", 0.05));
    return of(result);
  }
}
