import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationType } from '../confirmation/confirmation-type';
import { ConfirmationComponent } from '../confirmation/confirmation.component';
import { EmptyLanguageSample } from '../model/empty-language-sample';
import { EmptyMultilingualSample } from '../model/empty-multilingual-sample';
import { LanguageSample } from '../model/language-sample';
import { SampleRepositoryService } from '../services/sample-repository.service';

@Component({
  selector: 'lantor-language-sample-collection',
  templateUrl: './language-sample-collection.component.html',
  styleUrls: ['./language-sample-collection.component.css']
})
export class LanguageSampleCollectionComponent implements OnInit {
  public languageSamples: EmptyMultilingualSample | null = null;

  private collectionId = 0;
  public formGroup = new FormGroup({
    id: new FormControl(0),
    name: new FormControl(""),
    comment: new FormControl(""),
    languages: new FormControl<EmptyLanguageSample[]>([])
  });

  constructor(private sampleRepository: SampleRepositoryService, private router: Router, private route: ActivatedRoute, private modalService: NgbModal) { }

  ngOnInit() {
    const collectionIdParam = this.route.snapshot.paramMap.get("id");
    this.collectionId = collectionIdParam === null ? 0 : +collectionIdParam;

    this.refreshSamples();
  }

  refreshSamples() {
    if (this.collectionId === 0)
      return;

    this.sampleRepository.getMultilingualSamples().subscribe(s => {
      this.sampleRepository.getMultilingualSample(this.collectionId).subscribe(s => {
        if (s === null) return;

        this.languageSamples = s;
        this.formGroup.setValue(s);
      });
    });
  }

  getLanguages(): EmptyLanguageSample[] {
    return this.languageSamples === null ? [] : this.languageSamples.languages;
  }

  saveCollectionDetails() {
    const rawValue = this.formGroup.getRawValue();
    if (rawValue.id === null || rawValue.name === null || rawValue.comment === null || rawValue.languages === null) {
      console.log("Cannot process with null values.");
      return;
    }

    const mls: EmptyMultilingualSample = rawValue as EmptyMultilingualSample;
    if (mls.id === 0) {
      this.sampleRepository.createMultilingualSample(mls).subscribe((newMls) => {
        console.log("MultilingualSample created successfully.");
        this.languageSamples = newMls;
        this.formGroup.setValue(newMls);
        this.router.navigate(["/language-sample-collection", newMls.id]);
      });

    } else {
      this.sampleRepository.updateMultilingualSample(mls).subscribe(() => {
        console.log("MultilingualSample updated successfully.");
      }, (error: any) => {
        alert(`Error occurred while updating this resource: ${error.statusText}`);
      });
    }
  }

  cancelCollectionsDetails() {

  }

  openLanguageSample(id: number) {
    this.router.navigate(["/language-sample-collection", this.collectionId, 'language', id]);
  }

  removeLanguageSample(languageSample: EmptyLanguageSample) {
    console.log("Delete sample");
    let ref = this.modalService.open(ConfirmationComponent);
    ref.componentInstance.title = "Delete Sample";
    ref.componentInstance.messages = ["Would you like to delete the following sample:", languageSample.name, "This cannot be undone."];
    ref.componentInstance.confirmationType = ConfirmationType.YesNo;
    ref.componentInstance.mainAction = () => {
      this.sampleRepository.deleteLanguageSample(languageSample.id).subscribe(() => {
        this.refreshSamples();
      }, (error: any) => {
        alert(`Error occurred while deleting this resource: ${error.statusText}`);
      });
    };
  }

  addNewLanguage() {
    this.router.navigate(["/language-sample-collection", this.collectionId, 'language']);
  }

  addNewLanguageEnabled(): boolean {
    return this.languageSamples === null ? false : this.languageSamples.id !== 0;
  }

  onClickCrossButton() {
    this.router.navigate(["/configure"]);
  }
}
