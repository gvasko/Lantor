import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { CreateAlphabetComponent } from './create-alphabet.component';

describe('CreateAlphabetComponent', () => {
  let component: CreateAlphabetComponent;
  let fixture: ComponentFixture<CreateAlphabetComponent>;

  beforeEach(async () => {
    const modalSpy = jasmine.createSpyObj<NgbActiveModal>('NgbActiveModal', ['close', 'dismiss']);

    await TestBed.configureTestingModule({
      declarations: [CreateAlphabetComponent],
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

  it('maintains adjusted dimension %32', () => {
    const dimInput = fixture.nativeElement.querySelector('#dimension');
    const adjustedDim = fixture.nativeElement.querySelector('#adjusted-dimension');

    dimInput.value = '10';
    dimInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(adjustedDim.textContent).toBe('Adjusted dimension: 32');

    dimInput.value = '33';
    dimInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(adjustedDim.textContent).toBe('Adjusted dimension: 64');

    dimInput.value = '32';
    dimInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(adjustedDim.textContent).toBe('Adjusted dimension: 32');

    dimInput.value = '100';
    dimInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(adjustedDim.textContent).toBe('Adjusted dimension: 128');

  });

  it('enables create-button when both name and dimension are provided', () => {
    const createButton = fixture.nativeElement.querySelector('#create-button');
    expect(createButton.disabled).toBeTruthy();
    const nameInput = fixture.nativeElement.querySelector('#name');
    const dimInput = fixture.nativeElement.querySelector('#dimension');
    nameInput.value = 'fakename';
    nameInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(createButton.disabled).toBeTruthy();
    dimInput.value = '32';
    dimInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    expect(createButton.disabled).toBeFalsy();
  });

  it('does not show error at the beginning and create-button is disabled', () => {

  });

  it('shows error when name gets empty and create-button is disabled', () => {

  });

  it('hides error when name gets value back', () => {

  });

  it('shows error when dimension is not positive integer number and create-button is disabled', () => {

  });

  it('hides error when dimension gets valid value back', () => {

  });

  it('shows both errors when both name and dim are invalid and create-button is disabled', () => {

  });

  it('create-button is enabled when both name and dimenions are valid', () => {

  });

  it('closes when clicking create', () => {

  });

  it('dismisses when cancelled', () => {

  });

  it('dismisses when clicking X', () => {

  });
});
