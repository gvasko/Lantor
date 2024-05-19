import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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

  readonly MinDim: number = 1;
  readonly MaxDim: number = 10000;
  adjustedDimension: number | null = null;

  public formGroup = new FormGroup({
    id: new FormControl(0),
    name: new FormControl("", Validators.required),
    dim: new FormControl<number | null>(null, [Validators.required, Validators.min(this.MinDim), Validators.max(this.MaxDim)])
  });

  constructor(private activeModal: NgbActiveModal) {

  }

  isValueInvalid(fieldName: string): boolean {
    const field = this.formGroup.get(fieldName);
    if (!field) {
      throw "Unknown field: " + fieldName;
    }
    return field.invalid && field.touched;
  }

  dimChanged(event: any) {
    const dim = this.formGroup.controls.dim.value;
    if (!dim) return;

    const additional32 = dim % 32 === 0 ? 0 : 32;
    this.adjustedDimension = Math.floor(dim / 32) * 32 + additional32;
  }

  nameChanged(event: any) {

  }

  onClickCreateButton() {
    this.activeModal.close('Create click')
    let rawValue = this.formGroup.getRawValue();
    if (this.adjustedDimension) {
      rawValue.dim = this.adjustedDimension;
    }
    // TODO: check validity too
    if (this.createAction !== null) {
      this.createAction(rawValue as AlphabetListInfo);
    }
  }

  isFormInvalid(): boolean {
    return this.formGroup.invalid;
  }

  onClickCancelButton() {
    this.activeModal.dismiss('Cancel click')
  }

  onClickCrossButton() {
    this.activeModal.dismiss('Cross click')
  }
}
