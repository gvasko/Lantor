import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';
import { AlphabetListInfo } from '../model/alphabet-list-info';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';
import { SampleRepositoryService } from '../services/sample-repository.service';

import { LanguageAdminComponent } from './language-admin.component';

describe('LanguageAdminComponent', () => {
  let component: LanguageAdminComponent;
  let fixture: ComponentFixture<LanguageAdminComponent>;

  beforeEach(async () => {
    const modalSpy = jasmine.createSpyObj<NgbActiveModal>('NgbActiveModal', ['close', 'dismiss']);

    const tdSampleRepo = jasmine.createSpyObj('SampleReposiroryService', ['getMultilingualSamples', 'getAlphabets', 'deleteMultilingualSample', 'createAlphabet', 'deleteAlphabet']);

    const fakeAbc = new AlphabetListInfo(1, 'fakeabc', 10);
    tdSampleRepo.getAlphabets.and.returnValue(of([fakeAbc]));
    tdSampleRepo.createAlphabet.and.returnValue(of(fakeAbc));
    tdSampleRepo.deleteAlphabet.and.returnValue(of());

    const fakeSample = new MultilingualSampleListInfo(1, 'fakesample', 'comment', 10, 1);
    tdSampleRepo.getMultilingualSamples.and.returnValue(of([fakeSample]));
    tdSampleRepo.deleteMultilingualSample.and.returnValue(of());

    const tdRouter = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      declarations: [ LanguageAdminComponent ],
      imports: [FormsModule, ReactiveFormsModule],
      providers: [
        { provide: SampleRepositoryService, useValue: tdSampleRepo },
        { provide: Router, useValue: tdRouter },
        { provide: NgbActiveModal, useValue: modalSpy }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LanguageAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
