import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomDetectorComponent } from './custom-detector.component';

describe('CustomDetectorComponent', () => {
  let component: CustomDetectorComponent;
  let fixture: ComponentFixture<CustomDetectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomDetectorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CustomDetectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
