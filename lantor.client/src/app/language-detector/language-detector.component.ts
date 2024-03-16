import { Component } from '@angular/core';
import { LanguageSimilarityResult, LanguageSimilarityValue } from '../model/language-similarity-result';

@Component({
  selector: 'lantor-language-detector',
  templateUrl: './language-detector.component.html',
  styleUrls: ['./language-detector.component.css']
})
export class LanguageDetectorComponent {
  dimension: number = 10;
  text: string = "";
  result: LanguageSimilarityResult | null = null;

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
    let result = new LanguageSimilarityResult();
    result.similarityValues.push(new LanguageSimilarityValue("en", 0.15));
    result.similarityValues.push(new LanguageSimilarityValue("de", 0.11));
    result.similarityValues.push(new LanguageSimilarityValue("hu", 0.05));
    this.result = result;
  }
}
