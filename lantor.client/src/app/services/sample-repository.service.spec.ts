import { TestBed } from '@angular/core/testing';

import { SampleRepositoryService } from './sample-repository.service';

describe('SampleRepositoryService', () => {
  let service: SampleRepositoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SampleRepositoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
