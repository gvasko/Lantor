import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationType } from '../confirmation/confirmation-type';
import { ConfirmationComponent } from '../confirmation/confirmation.component';
import { CreateAlphabetComponent } from '../create-alphabet/create-alphabet.component';
import { AlphabetListInfo } from '../model/alphabet-list-info';
import { MultilingualSampleListInfo } from '../model/multilingual-sample-list-info';
import { SampleRepositoryService } from '../services/sample-repository.service';

@Component({
  selector: 'lantor-language-admin',
  templateUrl: './language-admin.component.html',
  styleUrls: ['./language-admin.component.css']
})
export class LanguageAdminComponent {
  languageSamples: MultilingualSampleListInfo[] = [];
  nullSample: MultilingualSampleListInfo = new MultilingualSampleListInfo(0, "Language Samples", "", 0);
  selectedSample: MultilingualSampleListInfo = this.nullSample;

  alphabets: AlphabetListInfo[] = [];
  nullAlphabet: AlphabetListInfo = new AlphabetListInfo(0, "Alphabets", 0);
  selectedAlphabet: AlphabetListInfo = this.nullAlphabet;

  constructor(private sampleRepository: SampleRepositoryService, private modalService: NgbModal, private router: Router) {
  }

  ngOnInit() {
    this.sampleRepository.getMultilingualSamples().subscribe(s => {
      this.languageSamples = s;
    });

    this.sampleRepository.getAlphabets().subscribe(abc => {
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
    this.router.navigate(["/language-sample-collection"]);
  }

  onOpenSelectedSample() {
    console.log("Open sample");
    this.router.navigate(["/language-sample-collection", this.selectedSample.id]);

  }

  onDeleteSelectedSample() {
    console.log("Delete sample");
    let ref = this.modalService.open(ConfirmationComponent);
    ref.componentInstance.title = "Delete Sample";
    ref.componentInstance.messages = ["Would you like to delete the following sample:", this.selectedSample.name, "This cannot be undone."];
    ref.componentInstance.confirmationType = ConfirmationType.YesNo;
    ref.componentInstance.mainAction = () => {
      console.log("DELETE SAMPLE");
    };
  }

  onCreateAlphabet() {
    let ref = this.modalService.open(CreateAlphabetComponent);
    ref.componentInstance.createAction = (abc: AlphabetListInfo) => {
      this.sampleRepository.createAlphabet(abc.name, abc.dim).subscribe((newAlphabet: AlphabetListInfo) => {
        this.alphabets.push(newAlphabet);
      });
    };
  }

  onDeleteSelectedAlphabet() {
    console.log("Delete aplhabet");
    let ref = this.modalService.open(ConfirmationComponent);
    ref.componentInstance.title = "Delete Alphabet";
    ref.componentInstance.messages = ["Would you like to delete the following alphabet:", this.selectedAlphabet.name, "This cannot be undone."];
    ref.componentInstance.confirmationType = ConfirmationType.YesNo;
    ref.componentInstance.mainAction = () => {
      console.log("DELETE ALPHABET");
    };
  }
}
