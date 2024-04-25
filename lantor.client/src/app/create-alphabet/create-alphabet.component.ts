import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ActionT } from '../services/action';

@Component({
  selector: 'lantor-create-alphabet',
  templateUrl: './create-alphabet.component.html',
  styleUrls: ['./create-alphabet.component.css']
})
export class CreateAlphabetComponent {
  @Input() public createAction: ActionT<number> | null = null;

  dimension: number | null = null;
  adjustedDimension: number | null = null;

  constructor(private activeModal: NgbActiveModal) {

  }

  dimChanged(event: any) {
    if (!this.dimension) return;

    const additional32 = this.dimension % 32 === 0 ? 0 : 32;
    this.adjustedDimension = Math.floor(this.dimension / 32) * 32 + additional32;
  }

  onClickCreateButton() {
    this.activeModal.close('Create click')
    if (this.createAction !== null) {
      this.createAction(this.adjustedDimension ?? 0);
    }
  }

  onClickCancelButton() {
    this.activeModal.dismiss('Cancel click')
  }

  onClickCrossButton() {
    this.activeModal.dismiss('Cross click')
  }
}
