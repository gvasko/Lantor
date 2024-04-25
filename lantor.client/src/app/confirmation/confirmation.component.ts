import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Action } from '../services/action';
import { ConfirmationType } from './confirmation-type';

@Component({
  selector: 'lantor-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.css']
})
export class ConfirmationComponent implements OnInit {

  @Input() public title: string = "";
  @Input() public messages: string[] = [ "" ];
  @Input() public confirmationType: ConfirmationType = ConfirmationType.YesNoCancel;
  @Input() public mainAction: Action | null = null;
  @Input() public secondaryAction: Action | null = null;
  @Input() public cancelAction: Action | null = null;

  public mainButtonLabel: string = "Main";
  public secondaryButtonLabel: string = "Secondary";
  public cancelButtonLabel: string = "Cancel";

  constructor(private activeModal: NgbActiveModal) {

  }

  ngOnInit() {
    switch (this.confirmationType) {
      case ConfirmationType.YesNo:
      case ConfirmationType.YesNoCancel: {
        this.mainButtonLabel = "Yes";
        this.secondaryButtonLabel = "No";
        this.cancelButtonLabel = "Cancel";
        break;
      }
    }
  }

  onClickMainButton() {
    this.activeModal.close('Main click')
    console.log("main-button clicked");
    if (this.mainAction !== null) {
      this.mainAction();
    }
  }

  onClickSecondaryButton() {
    this.activeModal.close('Secondary click')
    console.log("secondary-button clicked");
    if (this.secondaryAction !== null) {
      this.secondaryAction();
    }
  }

  onClickCancelButton() {
    this.activeModal.dismiss('Cancel click')
    console.log("cancel-button clicked");
    if (this.cancelAction !== null) {
      this.cancelAction();
    }
  }

  onClickCrossButton() {
    this.activeModal.dismiss('Cross click')
    console.log("cross-button clicked");
    if (this.cancelAction !== null) {
      this.cancelAction();
    }
  }

  cancelButtonAllowed(): boolean {
    return this.confirmationType === ConfirmationType.YesNoCancel;
  }

}
