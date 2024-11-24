import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';
import { SampleRepositoryService } from '../services/sample-repository.service';

import { LanguageSampleCollectionComponent } from './language-sample-collection.component';

describe('LanguageSampleCollectionComponent', () => {
  let component: LanguageSampleCollectionComponent;
  let fixture: ComponentFixture<LanguageSampleCollectionComponent>;

  beforeEach(async () => {
    const modalSpy = jasmine.createSpyObj<NgbActiveModal>('NgbActiveModal', ['close', 'dismiss']);

    const tdSampleRepo = jasmine.createSpyObj('SampleReposiroryService', ['createMultilingualSample', 'getMultilingualSample', 'getMultilingualSamples', 'updateMultilingualSample', 'deleteLanguageSample']);

    const fakeSample = new MultilingualSampleListInfo(1, 'fakesample', 'comment', 10, 1);
    tdSampleRepo.getMultilingualSample.and.returnValue(of(fakeSample));
    tdSampleRepo.getMultilingualSamples.and.returnValue(of([fakeSample]));
    tdSampleRepo.createMultilingualSample.and.returnValue(of(fakeSample));
    tdSampleRepo.updateMultilingualSample.and.returnValue(of());
    tdSampleRepo.deleteLanguageSample.and.returnValue(of());

    const tdRouter = jasmine.createSpyObj('Router', ['navigate']);

    const tdActivatedRoute = {
      snapshot: {
        paramMap: {
          get: () => 1
        }
      }
    };

    await TestBed.configureTestingModule({
      declarations: [LanguageSampleCollectionComponent],
      imports: [FormsModule, ReactiveFormsModule],
      providers: [
        { provide: SampleRepositoryService, useValue: tdSampleRepo },
        { provide: Router, useValue: tdRouter },
        { provide: ActivatedRoute, useValue: tdActivatedRoute },
        { provide: NgbActiveModal, useValue: modalSpy }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LanguageSampleCollectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
