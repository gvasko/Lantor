import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
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

  constructor(private sampleRepository: SampleRepositoryService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    const collectionIdParam = this.route.snapshot.paramMap.get("id");
    this.collectionId = collectionIdParam === null ? 0 : +collectionIdParam;

    this.sampleRepository.getMultilingualSamples().subscribe(s => {
      if (this.collectionId !== 0) {
        this.sampleRepository.getMultilingualSample(this.collectionId).subscribe(s => {
          if (s === null) return;

          this.languageSamples = s;
          this.formGroup.setValue(s);
        });
      }
    });
  }

  getLanguages(): EmptyLanguageSample[] {
    return this.languageSamples === null ? [] : this.languageSamples.languages;
  }

  saveCollectionDetails() {
    const rawValue = this.formGroup.getRawValue();
    if (rawValue.id === null || rawValue.name === null || rawValue.comment === null || rawValue.languages === null) {
      console.log("Cannot update with null values.");
      return;
    }
    const mls: EmptyMultilingualSample = rawValue as EmptyMultilingualSample;
    this.sampleRepository.updateMultilingualSample(mls).subscribe(() => {
      console.log("Updated successfully.");
    });
  }

  cancelCollectionsDetails() {

  }

  openLanguageSample(id: number) {
    this.router.navigate(["/language-sample-collection", this.collectionId, 'language', id]);

  }

  removeLanguageSample(id: number) {

  }

  addNewLanguage() {

  }

  onClickCrossButton() {
    this.router.navigate(["/language-admin"]);
  }
}
