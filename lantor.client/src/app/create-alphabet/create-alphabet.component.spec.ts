import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAlphabetComponent } from './create-alphabet.component';

describe('CreateAlphabetComponent', () => {
  let component: CreateAlphabetComponent;
  let fixture: ComponentFixture<CreateAlphabetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateAlphabetComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateAlphabetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
