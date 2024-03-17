import { TestBed } from '@angular/core/testing';

import { LanguageDetectorService } from './language-detector.service';

describe('LanguageDetectorService', () => {
  let service: LanguageDetectorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LanguageDetectorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
