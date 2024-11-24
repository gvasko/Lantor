import { HttpClient } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { EmptyMultilingualSample } from '../model/empty-multilingual-sample';
import { LanguageSample } from '../model/language-sample';

import { SampleRepositoryService } from './sample-repository.service';

describe('SampleRepositoryService', () => {
  let service: SampleRepositoryService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('HttpClient', ['get', 'post', 'put', 'delete']);

    TestBed.configureTestingModule({
      providers: [SampleRepositoryService, { provide: HttpClient, useValue: spy }]
    });

    service = TestBed.inject(SampleRepositoryService);
    httpClientSpy = TestBed.inject(HttpClient) as jasmine.SpyObj<HttpClient>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('getAlphabets invokes Alphabet API call', () => {
    service.getAlphabets();
    expect(httpClientSpy.get.calls.count()).toBe(1);
    expect(httpClientSpy.get.calls.mostRecent().args[0]).toBe(SampleRepositoryService.AlphabetApiUri);
  });

  it('createAlphabet invokes Alphabet API call', () => {
    service.createAlphabet('fake', 1);
    expect(httpClientSpy.post.calls.count()).toBe(1);
    expect(httpClientSpy.post.calls.mostRecent().args[0]).toBe(SampleRepositoryService.AlphabetApiUri);
  });

  it('deleteAlphabet invokes Alphabet API call', () => {
    service.deleteAlphabet(1);
    expect(httpClientSpy.delete.calls.count()).toBe(1);
    expect(httpClientSpy.delete.calls.mostRecent().args[0]).toMatch(new RegExp(`^${SampleRepositoryService.AlphabetApiUri}.*`));
  });

  it('getMultilingualSamples invokes MultilingualSample API call', () => {
    service.getMultilingualSamples();
    expect(httpClientSpy.get.calls.count()).toBe(1);
    expect(httpClientSpy.get.calls.mostRecent().args[0]).toBe(SampleRepositoryService.MultilingualSampleApiUri);
  });

  it('getMultilingualSample invokes MultilingualSample API call', () => {
    service.getMultilingualSample(1);
    expect(httpClientSpy.get.calls.count()).toBe(1);
    expect(httpClientSpy.get.calls.mostRecent().args[0]).toMatch(new RegExp(`^${SampleRepositoryService.MultilingualSampleApiUri}.*`));
  });

  it('updateMultilingualSample invokes MultilingualSample API call', () => {
    const fakeObj = new EmptyMultilingualSample(1, 'fake', 'comment', 1, []);
    service.updateMultilingualSample(fakeObj);
    expect(httpClientSpy.put.calls.count()).toBe(1);
    expect(httpClientSpy.put.calls.mostRecent().args[0]).toBe(SampleRepositoryService.MultilingualSampleApiUri);
  });

  it('createMultilingualSample invokes MultilingualSample API call', () => {
    const fakeObj = new EmptyMultilingualSample(1, 'fake', 'comment', 1, []);
    service.createMultilingualSample(fakeObj);
    expect(httpClientSpy.post.calls.count()).toBe(1);
    expect(httpClientSpy.post.calls.mostRecent().args[0]).toBe(SampleRepositoryService.MultilingualSampleApiUri);
  });

  it('deleteMultilingualSample invokes LanguageSample API call', () => {
    service.deleteMultilingualSample(1);
    expect(httpClientSpy.delete.calls.count()).toBe(1);
    expect(httpClientSpy.delete.calls.mostRecent().args[0]).toMatch(new RegExp(`^${SampleRepositoryService.MultilingualSampleApiUri}.*`));
  });

  it('getLanguageSample invokes LanguageSample API call', () => {
    service.getLanguageSample(1);
    expect(httpClientSpy.get.calls.count()).toBe(1);
    expect(httpClientSpy.get.calls.mostRecent().args[0]).toMatch(new RegExp(`^${SampleRepositoryService.LanguageSampleApiUri}.*`));
  });

  it('updateLanguageSample invokes LanguageSample API call', () => {
    const fakeObj = new LanguageSample(1, 'fake', 'sample', 1);
    service.updateLanguageSample(fakeObj);
    expect(httpClientSpy.put.calls.count()).toBe(1);
    expect(httpClientSpy.put.calls.mostRecent().args[0]).toBe(SampleRepositoryService.LanguageSampleApiUri);
  });

  it('createLanguageSample invokes LanguageSample API call', () => {
    const fakeObj = new LanguageSample(1, 'fake', 'sample', 1);
    service.createLanguageSample(fakeObj);
    expect(httpClientSpy.post.calls.count()).toBe(1);
    expect(httpClientSpy.post.calls.mostRecent().args[0]).toBe(SampleRepositoryService.LanguageSampleApiUri);
  });

  it('deleteLanguageSample invokes LanguageSample API call', () => {
    service.deleteLanguageSample(1);
    expect(httpClientSpy.delete.calls.count()).toBe(1);
    expect(httpClientSpy.delete.calls.mostRecent().args[0]).toMatch(new RegExp(`^${SampleRepositoryService.LanguageSampleApiUri}.*`));
  });

});
