import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefaultDetectorComponent } from './default-detector.component';

describe('DefaultDetectorComponent', () => {
  let component: DefaultDetectorComponent;
  let fixture: ComponentFixture<DefaultDetectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DefaultDetectorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DefaultDetectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
