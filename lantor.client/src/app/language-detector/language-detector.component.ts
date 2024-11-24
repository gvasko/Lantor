import { Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { AlphabetListInfo } from '../model/alphabet-list-info';
import { LanguageSimilarityResult, LanguageSimilarityValue } from '../model/language-similarity-result';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';
import { LanguageDetectorService } from '../services/language-detector.service';
import { SampleRepositoryService } from '../services/sample-repository.service';

@Component({
  selector: 'lantor-language-detector',
  templateUrl: './language-detector.component.html',
  styleUrls: ['./language-detector.component.css']
})
export class LanguageDetectorComponent {
  @Input() detectorConfigurationEnabled: boolean = false;

  text: string = "";
  result: LanguageSimilarityResult | null = null;
  languageSamples: MultilingualSampleListInfo[] = [];
  selectedSample: MultilingualSampleListInfo = new MultilingualSampleListInfo(0, "Language Samples", "", 0, 1);
  alphabets: AlphabetListInfo[] = [];
  selectedAlphabet: AlphabetListInfo = new AlphabetListInfo(0, "Alphabets", 0);

  constructor(private languageDetector: LanguageDetectorService, private sampleRepository: SampleRepositoryService) {

  }

  ngOnInit() {
    if (this.detectorConfigurationEnabled) {
      this.sampleRepository.getMultilingualSamples().subscribe(s => {
        this.languageSamples = s;
      });

      this.sampleRepository.getAlphabets().subscribe(abc => {
        this.alphabets = abc;
      });
    }
  }

  onSampleSelect(selected: MultilingualSampleListInfo) {
    this.selectedSample = selected;
  }

  onAlphabetSelect(selected: AlphabetListInfo) {
    this.selectedAlphabet = selected;
  }

  isSignificant(i: number): boolean {
    if (!this.result) {
      return false;
    }
    return i < this.result.significantCount;
  }

  onClear() {
    this.text = "";
    this.result = null;
  }

  get buttonsEnabled(): boolean {
    let enabled = true;
    if (this.detectorConfigurationEnabled) {
      enabled = this.selectedSample.id > 0 && this.selectedAlphabet.id > 0;
    }
    enabled = enabled && this.text !== ""
    return enabled;
  }

  onDetect() {
    let result: Observable<LanguageSimilarityResult>;
    if (this.detectorConfigurationEnabled) {
      if (this.selectedSample.id === 0) {
        throw "Invalid sample selection";
      }
      if (this.selectedAlphabet.id === 0) {
        throw "Invalid alphabet selection";
      }
      result = this.languageDetector.calculateCustomSimilarityValues(this.text, this.selectedSample.id, this.selectedAlphabet.id);
    } else {
      result = this.languageDetector.calculateDefaultSimilarityValues(this.text);
    }
    result.subscribe({
      next: (result: LanguageSimilarityResult) => {
        this.result = result;
      },
      error: (error) => {
        console.log(`Language detector error: ${error}`);
      }
    });
  }
}
