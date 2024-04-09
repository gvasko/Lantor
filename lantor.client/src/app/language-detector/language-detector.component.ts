import { Component, Input } from '@angular/core';
import { LanguageSimilarityResult, LanguageSimilarityValue } from '../model/language-similarity-result';
import { LanguageDetectorService } from '../services/language-detector.service';

@Component({
  selector: 'lantor-language-detector',
  templateUrl: './language-detector.component.html',
  styleUrls: ['./language-detector.component.css']
})
export class LanguageDetectorComponent {
  @Input() selectionEnabled: boolean = false;

  dimension: number = 10;
  text: string = "";
  result: LanguageSimilarityResult | null = null;
  languageSamples: string[] = ["Default", "Custom1"];
  selectedSample: string = "Language Samples";

  constructor(private languageDetector: LanguageDetectorService) {

  }

  onSampleSelect(selected: string) {
    this.selectedSample = selected;
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
    return this.text !== "";
  }

  onDetect() {
    this.languageDetector.calculateSimilarityValues(this.text).subscribe({
      next: (result: LanguageSimilarityResult) => {
        this.result = result;
      },
      error: (error) => {
        console.log(`Language detector error: ${error}`);
      }
    });
  }
}
