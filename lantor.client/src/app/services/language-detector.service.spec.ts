import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

import { LanguageDetectorService } from './language-detector.service';

describe('LanguageDetectorService', () => {
  let service: LanguageDetectorService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('HttpClient', ['post']);

    TestBed.configureTestingModule({
      imports: [],
      providers: [LanguageDetectorService, { provide: HttpClient, useValue: spy }]
    });
    service = TestBed.inject(LanguageDetectorService);
    httpClientSpy = TestBed.inject(HttpClient) as jasmine.SpyObj<HttpClient>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('default similarity values should invoke API call', () => {
    service.calculateDefaultSimilarityValues("fake sample text");
    expect(httpClientSpy.post.calls.count()).toBe(1);
  });

  it('custom similarity values should invoke API call', () => {
    service.calculateCustomSimilarityValues("fake sample text", 1, 1);
    expect(httpClientSpy.post.calls.count()).toBe(1);
  });

  // TODO: cover error handling too
});
