import { Component } from '@angular/core';
import { LoaderService } from '../services/loader.service';

@Component({
  selector: 'lantor-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent {
  constructor(public loader: LoaderService) { }
}
