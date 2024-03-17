import { Component } from '@angular/core';
import { LanguageSimilarityResult, LanguageSimilarityValue } from '../model/language-similarity-result';
import { LanguageDetectorService } from '../services/language-detector.service';

@Component({
  selector: 'lantor-language-detector',
  templateUrl: './language-detector.component.html',
  styleUrls: ['./language-detector.component.css']
})
export class LanguageDetectorComponent {
  dimension: number = 10;
  text: string = "";
  result: LanguageSimilarityResult | null = null;

  constructor(private languageDetector: LanguageDetectorService) {

  }

  get samplesSelectorEnabled(): boolean {
    return false;
  }

  get dimensionsEnabled(): boolean {
    return false;
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
