import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { of } from 'rxjs';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';
import { SampleRepositoryService } from '../services/sample-repository.service';
import { LanguageSampleComponent } from './language-sample.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

describe('LanguageSampleComponent', () => {
  let component: LanguageSampleComponent;
  let fixture: ComponentFixture<LanguageSampleComponent>;

  beforeEach(async () => {
    const tdSampleRepo = jasmine.createSpyObj('SampleReposiroryService', ['getLanguageSample', 'createLanguageSample', 'updateLanguageSample']);

    const fakeSample = new MultilingualSampleListInfo(1, 'fakesample', 'comment', 10);
    tdSampleRepo.getLanguageSample.and.returnValue(of(fakeSample));
    tdSampleRepo.createLanguageSample.and.returnValue(of(fakeSample));
    tdSampleRepo.updateLanguageSample.and.returnValue(of());

    const tdRouter = jasmine.createSpyObj('Router', ['navigate']);

    const tdActivatedRoute = {
      snapshot: {
        paramMap: {
          get: () => 1
        }
      }
    };

    await TestBed.configureTestingModule({
      declarations: [LanguageSampleComponent],
      imports: [FormsModule, ReactiveFormsModule],
      providers: [
        { provide: SampleRepositoryService, useValue: tdSampleRepo },
        { provide: Router, useValue: tdRouter },
        { provide: ActivatedRoute, useValue: tdActivatedRoute }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LanguageSampleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
