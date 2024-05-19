import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { CreateAlphabetComponent } from './create-alphabet.component';

describe('CreateAlphabetComponent', () => {
  let component: CreateAlphabetComponent;
  let fixture: ComponentFixture<CreateAlphabetComponent>;
  let modalSpy: jasmine.SpyObj<NgbActiveModal>;
  let nameInput: any;
  let dimInput: any;
  let adjustedDim: any;
  let createButton: any;
  let cancelButton: any;
  let xButton: any;

  beforeEach(async () => {
    modalSpy = jasmine.createSpyObj<NgbActiveModal>('NgbActiveModal', ['close', 'dismiss']);

    await TestBed.configureTestingModule({
      declarations: [CreateAlphabetComponent],
      imports: [FormsModule],
      providers: [{ provide: NgbActiveModal, useValue: modalSpy }]
    })
      .compileComponents();

    fixture = TestBed.createComponent(CreateAlphabetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    nameInput = fixture.nativeElement.querySelector('#name');
    dimInput = fixture.nativeElement.querySelector('#dimension');
    adjustedDim = fixture.nativeElement.querySelector('#adjusted-dimension');
    createButton = fixture.nativeElement.querySelector('#create-button');
    cancelButton = fixture.nativeElement.querySelector('#cancel-button');
    xButton = fixture.nativeElement.querySelector('#x-button');
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('maintains adjusted dimension %32', () => {
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
    expect(createButton.disabled).toBeTruthy();
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
    // provide invalid values
  });

  it('closes when clicking create', () => {
    nameInput.value = 'fakename';
    nameInput.dispatchEvent(new Event('input'));
    dimInput.value = '32';
    dimInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();
    createButton.click();
    expect(modalSpy.close.calls.count()).toBe(1);
  });

  it('dismisses when cancelled', () => {
    cancelButton.click();
    expect(modalSpy.dismiss.calls.count()).toBe(1);
  });

  it('dismisses when clicking X', () => {
    xButton.click();
    expect(modalSpy.dismiss.calls.count()).toBe(1);
  });
});
