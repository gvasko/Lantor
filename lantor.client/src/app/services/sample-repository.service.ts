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
  private static AlphabetApiUri = '/api/alphabet';
  private static MultilingualSampleApiUri = '/api/multilingualsample';
  private static LanguageSampleApiUri = '/api/languagesample';

  getAlphabets(): Observable<AlphabetListInfo[]> {
    return this.http.get<AlphabetListInfo[]>(SampleRepositoryService.AlphabetApiUri);
  }

  deleteAlphabet(id: number): Observable<void> {
    return this.http.delete<void>(`${SampleRepositoryService.AlphabetApiUri}/${id}`);
  }

  createAlphabet(name: string, dim: number): Observable<AlphabetListInfo> {
    return this.http.post<AlphabetListInfo>(SampleRepositoryService.AlphabetApiUri, new AlphabetListInfo(0, name, dim));
  }

  getMultilingualSamples(): Observable<MultilingualSampleListInfo[]> {
    return this.http.get<MultilingualSampleListInfo[]>(SampleRepositoryService.MultilingualSampleApiUri);
  }

  getMultilingualSample(id: number): Observable<EmptyMultilingualSample | null> {
    return this.http.get<EmptyMultilingualSample>(`${SampleRepositoryService.MultilingualSampleApiUri}/${id}`);
  }

  updateMultilingualSample(mls: EmptyMultilingualSample): Observable<void> {
    return this.http.put<void>(SampleRepositoryService.MultilingualSampleApiUri, mls);
  }

  createMultilingualSample(mls: EmptyMultilingualSample): Observable<EmptyMultilingualSample> {
    return this.http.post<EmptyMultilingualSample>(SampleRepositoryService.MultilingualSampleApiUri, mls);
  }

  deleteMultilingualSample(id: number): Observable<void> {
    return this.http.delete<void>(`${SampleRepositoryService.MultilingualSampleApiUri}/${id}`);
  }

  getLanguageSample(id: number): Observable<LanguageSample | null> {
    return this.http.get<LanguageSample>(`${SampleRepositoryService.LanguageSampleApiUri}/${id}`);
  }

  updateLanguageSample(ls: LanguageSample): Observable<void> {
    return this.http.put<void>(SampleRepositoryService.LanguageSampleApiUri, ls);
  }

  createLanguageSample(ls: LanguageSample): Observable<LanguageSample> {
    return this.http.post<LanguageSample>(SampleRepositoryService.LanguageSampleApiUri, ls);
  }

  deleteLanguageSample(id: number): Observable<void> {
    return this.http.delete<void>(`${SampleRepositoryService.LanguageSampleApiUri}/${id}`);
  }

}
