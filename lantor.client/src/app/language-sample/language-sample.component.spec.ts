import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LanguageSampleComponent } from './language-sample.component';

describe('LanguageSampleComponent', () => {
  let component: LanguageSampleComponent;
  let fixture: ComponentFixture<LanguageSampleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LanguageSampleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LanguageSampleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
