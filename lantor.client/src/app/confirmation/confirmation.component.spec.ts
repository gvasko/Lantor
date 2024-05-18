import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Action } from '../services/action';
import { ConfirmationType } from './confirmation-type';

import { ConfirmationComponent } from './confirmation.component';

describe('ConfirmationComponent', () => {
  let component: ConfirmationComponent;
  let fixture: ComponentFixture<ConfirmationComponent>;
  let activeModalSpy: jasmine.SpyObj<NgbActiveModal>;
  let mainActionClicked = false;
  let secondaryActionClicked = false;
  let cancelActionClicked = false;

  beforeEach(async () => {
    const modalSpy = jasmine.createSpyObj<NgbActiveModal>('NgbActiveModal', ['close', 'dismiss']);

    mainActionClicked = false;
    secondaryActionClicked = false;
    cancelActionClicked = false;

    await TestBed.configureTestingModule({
      declarations: [ConfirmationComponent],
      providers: [{ provide: NgbActiveModal, useValue: modalSpy }]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConfirmationComponent);
    component = fixture.componentInstance;
    activeModalSpy = modalSpy;
    component.title = "FakeTitle";
    component.messages = ["Message1", "Message2"];
    component.mainAction = () => { mainActionClicked = true; };
    component.secondaryAction = () => { secondaryActionClicked = true; };
    component.cancelAction = () => { cancelActionClicked = true; };
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should show the title', () => {
    let modalTitleDiv = fixture.nativeElement.querySelector('#modal-title');
    expect(modalTitleDiv.textContent).toBe("FakeTitle");
  });

  it('should show the messages', () => {
    let modalMessagesParas = fixture.nativeElement.querySelectorAll('#modal-messages > p');
    expect(modalMessagesParas.length).toBe(2);
    expect(modalMessagesParas[0].textContent).toBe("Message1");
    expect(modalMessagesParas[1].textContent).toBe("Message2");
  });

  it('should have 3 buttons by default', () => {
    let buttonMain = fixture.nativeElement.querySelector('#modal-button-main');
    expect(buttonMain).not.toBe(null);
    let buttonSecondary = fixture.nativeElement.querySelector('#modal-button-secondary');
    expect(buttonSecondary).not.toBe(null);
    let buttonCancel = fixture.nativeElement.querySelector('#modal-button-cancel');
    expect(buttonCancel).not.toBe(null);
  });

  it('should support 2 buttons', () => {
    component.confirmationType = ConfirmationType.YesNo;
    fixture.detectChanges();
    let buttonMain = fixture.nativeElement.querySelector('#modal-button-main');
    expect(buttonMain).not.toBe(null);
    let buttonSecondary = fixture.nativeElement.querySelector('#modal-button-secondary');
    expect(buttonSecondary).not.toBe(null);
    let buttonCancel = fixture.nativeElement.querySelector('#modal-button-cancel');
    expect(buttonCancel).toBe(null);
  });

  it('invokes the action when clicks on the main button', () => {
    let button = fixture.nativeElement.querySelector('#modal-button-main');
    button.click();
    expect(mainActionClicked).toBeTruthy();
    expect(secondaryActionClicked).toBeFalsy();
    expect(cancelActionClicked).toBeFalsy();
  });

  it('invokes the action when clicks on the secondary button', () => {
    let button = fixture.nativeElement.querySelector('#modal-button-secondary');
    button.click();
    expect(mainActionClicked).toBeFalsy();
    expect(secondaryActionClicked).toBeTruthy();
    expect(cancelActionClicked).toBeFalsy();
  });

  it('invokes the action when clicks on the cancel button', () => {
    let button = fixture.nativeElement.querySelector('#modal-button-cancel');
    button.click();
    expect(mainActionClicked).toBeFalsy();
    expect(secondaryActionClicked).toBeFalsy();
    expect(cancelActionClicked).toBeTruthy();
  });

  it('invokes the action when clicks on the X button', () => {
    let button = fixture.nativeElement.querySelector('#modal-button-x');
    button.click();
    expect(mainActionClicked).toBeFalsy();
    expect(secondaryActionClicked).toBeFalsy();
    expect(cancelActionClicked).toBeTruthy();
  });
});
