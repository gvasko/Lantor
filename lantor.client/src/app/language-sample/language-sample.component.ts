import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LanguageSample } from '../model/language-sample';
import { SampleRepositoryService } from '../services/sample-repository.service';

@Component({
  selector: 'lantor-language-sample',
  templateUrl: './language-sample.component.html',
  styleUrls: ['./language-sample.component.css']
})
export class LanguageSampleComponent implements OnInit {

  private collectionId = 0;
  private languageId = 0;
  public formGroup = new FormGroup({
    id: new FormControl(0),
    name: new FormControl(""),
    sample: new FormControl(""),
    multilingualSampleId: new FormControl(0)
  });


  constructor(private sampleRepository: SampleRepositoryService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    const collectionIdParam = this.route.snapshot.paramMap.get("id");
    this.collectionId = collectionIdParam === null ? 0 : +collectionIdParam;

    const languageIdParam = this.route.snapshot.paramMap.get("languageId");
    this.languageId = languageIdParam === null ? 0 : +languageIdParam;

    if (this.languageId > 0) {
      this.sampleRepository.getLanguageSample(this.languageId).subscribe(ls => {
        if (ls === null) return;
        this.formGroup.setValue(ls);
      });
    } else {
      this.formGroup.controls.multilingualSampleId.setValue(this.collectionId);
    }

  }

  saveLanguageSampleDetails() {
    const rawValue = this.formGroup.getRawValue();
    if (rawValue.id === null || rawValue.name === null || rawValue.sample === null || rawValue.multilingualSampleId === null) {
      console.log("Cannot process with null values.");
      return;
    }

    const ls: LanguageSample = rawValue as LanguageSample;
    if (ls.id === 0) {
      this.sampleRepository.createLanguageSample(ls).subscribe((newLs) => {
        console.log("LanguageSample created successfully.");
        this.formGroup.setValue(newLs);
        this.router.navigate(["/language-sample-collection", this.collectionId, 'language', newLs.id]);
      })
    } else {
      this.sampleRepository.updateLanguageSample(ls).subscribe(() => {
        console.log("LanguageSample updated successfully.");
      }, (error: any) => {
        alert(`Error occurred while updating this resource: ${error.statusText}`);
      });
    }
  }

  cancelLanguageSampleDetails() {
    this.goBackToCollection();
  }

  private goBackToCollection() {
    this.router.navigate(["/language-sample-collection", this.collectionId]);

  }
}
