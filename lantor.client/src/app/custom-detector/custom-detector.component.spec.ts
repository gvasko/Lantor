import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { LanguageDetectorComponent } from '../language-detector/language-detector.component';
import { AlphabetListInfo } from '../model/alphabet-list-info';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';
import { LanguageDetectorService } from '../services/language-detector.service';
import { SampleRepositoryService } from '../services/sample-repository.service';

import { CustomDetectorComponent } from './custom-detector.component';

describe('CustomDetectorComponent', () => {
  let component: CustomDetectorComponent;
  let fixture: ComponentFixture<CustomDetectorComponent>;

  beforeEach(async () => {
    const tdLangDetService = jasmine.createSpyObj('LanguageDetectorService', ['calculateDefaultSimilarityValues', 'calculateCustomSimilarityValues']);

    const tdSampleRepo = jasmine.createSpyObj('SampleReposiroryService', ['getAlphabets', 'getMultilingualSamples']);

    const fakeAbc = new AlphabetListInfo(1, 'fakeabc', 10);
    tdSampleRepo.getAlphabets.and.returnValue(of([fakeAbc]));

    const fakeSample = new MultilingualSampleListInfo(1, 'fakesample', 'comment', 10);
    tdSampleRepo.getMultilingualSamples.and.returnValue(of([fakeSample]));

    await TestBed.configureTestingModule({
      declarations: [CustomDetectorComponent, LanguageDetectorComponent],
      imports: [FormsModule],
      providers: [
        { provide: LanguageDetectorService, useValue: tdLangDetService },
        { provide: SampleRepositoryService, useValue: tdSampleRepo }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomDetectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
