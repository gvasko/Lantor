import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LanguageSample } from '../model/language-sample';
import { MultilingualSample } from '../model/multilingual-sample';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';
import { LanguageDetectorService } from '../services/language-detector.service';

@Component({
  selector: 'lantor-sample-collection',
  templateUrl: './sample-collection.component.html',
  styleUrls: ['./sample-collection.component.css']
})
export class SampleCollectionComponent implements OnInit {
  public selectedLanguageSample: LanguageSample | null = null;
  public languageSamples: MultilingualSample | null = null;

  constructor(private languageDetector: LanguageDetectorService, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.languageDetector.getMultilingualSamples().subscribe(s => {
      const sampleIdParam = this.route.snapshot.paramMap.get("selectedSampleId");
      const sampleId = sampleIdParam === null ? 0 : +sampleIdParam;

      if (sampleId !== 0) {
        this.languageDetector.getMultilingualSample(sampleId).subscribe(s => {
          this.languageSamples = s;
        });
      }
    });
  }

  getLanguages(): LanguageSample[] {
    return this.languageSamples === null ? [] : this.languageSamples.languages;
  }

  onSampleSelect(sampleId: number) {
    const found = this.getLanguages().find(ls => ls.id === sampleId);
    if (!!found) {
      this.selectedLanguageSample = found;
    } else {
      this.selectedLanguageSample = null;
    }
  }

  createNewSample() {
    const newLang = new LanguageSample(0, "New language", "New sample");
    this.languageSamples?.languages.push(newLang);
    this.selectedLanguageSample = newLang;
  }

  onClickDeleteSelectedLanguage() {

  }

  onClickSaveButton() {
    this.closeComponent();
  }

  onClickCancelButton() {
    this.closeComponent();
  }

  onClickCrossButton() {
    this.closeComponent();
  }

  private closeComponent() {
    this.router.navigateByUrl("/language-admin");
  }

}
