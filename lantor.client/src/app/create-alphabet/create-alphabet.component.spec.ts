import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { CreateAlphabetComponent } from './create-alphabet.component';

describe('CreateAlphabetComponent', () => {
  let component: CreateAlphabetComponent;
  let fixture: ComponentFixture<CreateAlphabetComponent>;

  beforeEach(async () => {
    const modalSpy = jasmine.createSpyObj<NgbActiveModal>('NgbActiveModal', ['close', 'dismiss']);

    await TestBed.configureTestingModule({
      declarations: [ CreateAlphabetComponent ],
      imports: [FormsModule],
      providers: [{ provide: NgbActiveModal, useValue: modalSpy }]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateAlphabetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
