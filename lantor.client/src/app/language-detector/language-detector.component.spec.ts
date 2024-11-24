import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { AlphabetListInfo } from '../model/alphabet-list-info';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';
import { LanguageDetectorService } from '../services/language-detector.service';
import { SampleRepositoryService } from '../services/sample-repository.service';
import { FormsModule } from '@angular/forms';

import { LanguageDetectorComponent } from './language-detector.component';

describe('LanguageDetectorComponent', () => {
  let component: LanguageDetectorComponent;
  let fixture: ComponentFixture<LanguageDetectorComponent>;

  beforeEach(async () => {
    const tdLangDetService = jasmine.createSpyObj('LanguageDetectorService', ['calculateDefaultSimilarityValues', 'calculateCustomSimilarityValues']);

    const tdSampleRepo = jasmine.createSpyObj('SampleReposiroryService', ['getAlphabets', 'getMultilingualSamples']);

    const fakeAbc = new AlphabetListInfo(1, 'fakeabc', 10);
    tdSampleRepo.getAlphabets.and.returnValue(of([fakeAbc]));

    const fakeSample = new MultilingualSampleListInfo(1, 'fakesample', 'comment', 10, 1);
    tdSampleRepo.getMultilingualSamples.and.returnValue(of([fakeSample]));

    await TestBed.configureTestingModule({
      declarations: [LanguageDetectorComponent],
      imports: [FormsModule],
      providers: [
        { provide: LanguageDetectorService, useValue: tdLangDetService },
        { provide: SampleRepositoryService, useValue: tdSampleRepo }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LanguageDetectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
