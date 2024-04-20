import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LanguageSample } from '../model/language-sample';
import { MultilingualSample } from '../model/multilingual-sample';
import { LanguageDetectorService } from '../services/language-detector.service';

@Component({
  selector: 'lantor-language-sample-collection',
  templateUrl: './language-sample-collection.component.html',
  styleUrls: ['./language-sample-collection.component.css']
})
export class LanguageSampleCollectionComponent implements OnInit {
  public languageSamples: MultilingualSample | null = null;

  private collectionId = 0;
  constructor(private languageDetector: LanguageDetectorService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    const collectionIdParam = this.route.snapshot.paramMap.get("id");
    this.collectionId = collectionIdParam === null ? 0 : +collectionIdParam;

    this.languageDetector.getMultilingualSamples().subscribe(s => {
      if (this.collectionId !== 0) {
        this.languageDetector.getMultilingualSample(this.collectionId).subscribe(s => {
          if (s === null) return;

          this.languageSamples = s;
        });
      }
    });
  }

  getLanguages(): LanguageSample[] {
    return this.languageSamples === null ? [] : this.languageSamples.languages;
  }

  saveCollectionDetails() {

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
