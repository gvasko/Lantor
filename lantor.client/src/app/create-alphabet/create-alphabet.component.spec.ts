import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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

  const changeInput = (inputElement: any, value: string) => {
    inputElement.dispatchEvent(new Event('focus'));
    inputElement.value = value;
    inputElement.dispatchEvent(new Event('input'));
    inputElement.dispatchEvent(new Event('blur'));
    fixture.detectChanges();
  };

  const setupSimpleValidState = () => {
    nameInput.value = 'dummy';
    nameInput.dispatchEvent(new Event('input'));
    dimInput.value = '22';
    dimInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();
  }

  const expectDimWarning = () => {
    let dimWarning = fixture.nativeElement.querySelector('#dim-warning');
    expect(dimWarning).not.toBeNull();
  }

  const expectNoDimWarning = () => {
    let dimWarning = fixture.nativeElement.querySelector('#dim-warning');
    expect(dimWarning).toBeNull();
  }

  const expectNameWarning = () => {
    let nameWarning = fixture.nativeElement.querySelector('#name-warning');
    expect(nameWarning).not.toBeNull();
  }

  const expectNoNameWarning = () => {
    let nameWarning = fixture.nativeElement.querySelector('#name-warning');
    expect(nameWarning).toBeNull();
  }

  const expectCreateButtonIsDisabled = () => {
    expect(createButton.disabled).toBeTruthy();
  }

  const expectCreateButtonIsEnabled = () => {
    expect(createButton.disabled).toBeFalsy();
  }

  beforeEach(async () => {
    modalSpy = jasmine.createSpyObj<NgbActiveModal>('NgbActiveModal', ['close', 'dismiss']);

    await TestBed.configureTestingModule({
      declarations: [CreateAlphabetComponent],
      imports: [FormsModule, ReactiveFormsModule],
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
    changeInput(dimInput, '10');
    expect(adjustedDim.textContent).toBe('Adjusted dimension: 32');

    changeInput(dimInput, '33');
    expect(adjustedDim.textContent).toBe('Adjusted dimension: 64');

    changeInput(dimInput, '32');
    expect(adjustedDim.textContent).toBe('Adjusted dimension: 32');

    changeInput(dimInput, '100');
    expect(adjustedDim.textContent).toBe('Adjusted dimension: 128');

  });

  it('enables create-button when both name and dimension are provided', () => {
    expectCreateButtonIsDisabled();
    changeInput(nameInput, 'dummy');
    expectCreateButtonIsDisabled();
    changeInput(dimInput, '22');
    expectCreateButtonIsEnabled();
  });

  it('starts with empty inputs', () => {
    expect(nameInput.textContent).toBe('');
    expect(dimInput.textContent).toBe('');
  });

  it('removes adjusted dim when input gets empty', () => {
    expect(adjustedDim.textContent).toBe('Adjusted dimension: ');
    changeInput(dimInput, '22');
    expect(adjustedDim.textContent).toBe('Adjusted dimension: 32');
    changeInput(dimInput, '');
    expect(adjustedDim.textContent).toBe('Adjusted dimension: ');
  });

  it('does not show error and create-button is disabled at the beginning', () => {
    expectNoNameWarning();
    expectNoDimWarning();
    expectCreateButtonIsDisabled();
  });

  it('shows error and create-button is disabled when name gets empty', () => {
    setupSimpleValidState();
    changeInput(nameInput, '');
    expectNameWarning();
    expectCreateButtonIsDisabled();
  });

  it('hides error when name gets value back', () => {
    setupSimpleValidState();
    changeInput(nameInput, '');
    changeInput(nameInput, 'dummy2');
    expectNoNameWarning();
    expectCreateButtonIsEnabled();
  });

  it('shows error when dimension is not positive integer number and create-button is disabled', () => {
    setupSimpleValidState();
    changeInput(dimInput, '-1');
    expectDimWarning();
    expectCreateButtonIsDisabled();
  });

  it('hides error when dimension gets valid value back', () => {
    setupSimpleValidState();
    changeInput(dimInput, '-1');
    changeInput(dimInput, '1');
    expectNoDimWarning();
    expectCreateButtonIsEnabled();
  });

  it('shows both errors when both name and dim are invalid and create-button is disabled', () => {
    setupSimpleValidState();
    changeInput(nameInput, '');
    changeInput(dimInput, '-1');
    expectNameWarning();
    expectDimWarning();
    expectCreateButtonIsDisabled();
  });

  it('create-button is enabled when both name and dimenions are valid', () => {
    setupSimpleValidState();
    changeInput(nameInput, '');
    changeInput(dimInput, '-1');
    changeInput(nameInput, 'dummy2');
    changeInput(dimInput, '1');
    expectNoNameWarning();
    expectNoDimWarning();
    expectCreateButtonIsEnabled();
  });

  it('closes when clicking create', () => {
    setupSimpleValidState();
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
