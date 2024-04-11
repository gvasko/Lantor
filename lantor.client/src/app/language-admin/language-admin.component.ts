import { Component } from '@angular/core';
import { AlphabetListInfo } from '../model/alphabet-list-info';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';
import { LanguageDetectorService } from '../services/language-detector.service';

@Component({
  selector: 'lantor-language-admin',
  templateUrl: './language-admin.component.html',
  styleUrls: ['./language-admin.component.css']
})
export class LanguageAdminComponent {
  languageSamples: MultilingualSampleListInfo[] = [];
  nullSample: MultilingualSampleListInfo = new MultilingualSampleListInfo(0, "Language Samples", 0);
  selectedSample: MultilingualSampleListInfo = this.nullSample;

  alphabets: AlphabetListInfo[] = [];
  nullAlphabet: AlphabetListInfo = new AlphabetListInfo(0, "Alphabets", 0);
  selectedAlphabet: AlphabetListInfo = this.nullAlphabet;

  constructor(private languageDetector: LanguageDetectorService) {
  }

  ngOnInit() {
    this.languageDetector.getMultilingualSamples().subscribe(s => {
      this.languageSamples = s;
    });

    this.languageDetector.getAlphabets().subscribe(abc => {
      this.alphabets = abc;
    });
  }

  onSampleSelect(selected: MultilingualSampleListInfo) {
    if (selected.id === this.selectedSample.id) {
      this.selectedSample = this.nullSample;
    } else {
      this.selectedSample = selected;
    }
  }

  onAlphabetSelect(selected: AlphabetListInfo) {
    if (selected.id === this.selectedAlphabet.id) {
      this.selectedAlphabet = this.nullAlphabet;
    } else {
      this.selectedAlphabet = selected;
    }
  }

  isSelectedSample(sample: MultilingualSampleListInfo): boolean {
    return this.selectedSample.id === sample.id;
  }

  isSelectedAlphabet(alphabet: AlphabetListInfo): boolean {
    return this.selectedAlphabet.id === alphabet.id;
  }

  onCreateSample() {
    console.log("Create sample");
  }

  onOpenSelectedSample() {
    console.log("Open sample");

  }

  onDeleteSelectedSample() {
    console.log("Delete sample");

  }

  onCreateAlphabet() {
    console.log("Create alphabet");
  }

  onDeleteSelectedAlphabet() {
    console.log("Delete aplhabet");

  }
}
