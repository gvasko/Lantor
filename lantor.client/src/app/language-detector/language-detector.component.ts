import { Component } from '@angular/core';

@Component({
  selector: 'lantor-language-detector',
  templateUrl: './language-detector.component.html',
  styleUrls: ['./language-detector.component.css']
})
export class LanguageDetectorComponent {
  dimension: number = 10;
  get samplesSelectorEnabled(): boolean {
    return false;
  }

  get dimensionsEnabled(): boolean {
    return false;
  }
}
