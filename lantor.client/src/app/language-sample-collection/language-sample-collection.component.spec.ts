import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguageSampleCollectionComponent } from './language-sample-collection.component';

describe('LanguageSampleCollectionComponent', () => {
  let component: LanguageSampleCollectionComponent;
  let fixture: ComponentFixture<LanguageSampleCollectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LanguageSampleCollectionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LanguageSampleCollectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
