import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlphabetListInfo } from '../model/alphabet-list-info';
import { ActionT } from '../services/action';

@Component({
  selector: 'lantor-create-alphabet',
  templateUrl: './create-alphabet.component.html',
  styleUrls: ['./create-alphabet.component.css']
})
export class CreateAlphabetComponent {
  @Input() public createAction: ActionT<AlphabetListInfo> | null = null;

  readonly MinDim: number = 32;
  readonly MaxDim: number = 10016;
  dimension: number | null = null;
  adjustedDimension: number | null = null;
  alphabetName: string | null = null;

  constructor(private activeModal: NgbActiveModal) {

  }

  dimChanged(event: any) {
    if (!this.dimension) return;

    const additional32 = this.dimension % 32 === 0 ? 0 : 32;
    this.adjustedDimension = Math.floor(this.dimension / 32) * 32 + additional32;
  }

  nameChanged(event: any) {

  }

  onClickCreateButton() {
    this.activeModal.close('Create click')
    if (this.createAction !== null && this.createEnabled()) {
      this.createAction(new AlphabetListInfo(0, this.alphabetName!, this.adjustedDimension!));
    }
  }

  createEnabled(): boolean {
    return this.alphabetName !== null && this.adjustedDimension !== null;
  }

  onClickCancelButton() {
    this.activeModal.dismiss('Cancel click')
  }

  onClickCrossButton() {
    this.activeModal.dismiss('Cross click')
  }
}
