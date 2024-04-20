import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LanguageDetectorService } from '../services/language-detector.service';

@Component({
  selector: 'lantor-language-sample',
  templateUrl: './language-sample.component.html',
  styleUrls: ['./language-sample.component.css']
})
export class LanguageSampleComponent implements OnInit {

  private collectionId = 0;
  private languageId = 0;
  constructor(private languageDetector: LanguageDetectorService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    const collectionIdParam = this.route.snapshot.paramMap.get("id");
    this.collectionId = collectionIdParam === null ? 0 : +collectionIdParam;

    const languageIdParam = this.route.snapshot.paramMap.get("languageId");
    this.languageId = languageIdParam === null ? 0 : +languageIdParam;

  }

  saveLanguageSampleDetails() {

  }

  cancelLanguageSampleDetails() {
    this.goBackToCollection();
  }

  private goBackToCollection() {
    this.router.navigate(["/language-sample-collection", this.collectionId]);

  }
}
