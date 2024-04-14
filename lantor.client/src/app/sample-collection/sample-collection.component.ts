import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
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
  @Input() selectedSample: MultilingualSampleListInfo | null = null;

  public selectedLanguageSample: LanguageSample | null = null;
  private languageSamples: MultilingualSample | null = null;

  constructor(private languageDetector: LanguageDetectorService, private activeModal: NgbActiveModal) {

  }

  ngOnInit() {
    this.languageDetector.getMultilingualSamples().subscribe(s => {
      if (this.selectedSample !== null) {
        this.languageDetector.getMultilingualSample(this.selectedSample.id).subscribe(s => {
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
    this.activeModal.close('Save click');
  }

  onClickCancelButton() {
    this.activeModal.close('Cancel click');

  }

  onClickCrossButton() {
    this.activeModal.close('Cross-button click');

  }

}
