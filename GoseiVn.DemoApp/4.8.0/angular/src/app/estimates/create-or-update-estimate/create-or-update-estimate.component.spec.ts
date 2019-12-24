import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateOrUpdateEstimateComponent } from './create-or-update-estimate.component';

describe('CreateOrUpdateEstimateComponent', () => {
  let component: CreateOrUpdateEstimateComponent;
  let fixture: ComponentFixture<CreateOrUpdateEstimateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateOrUpdateEstimateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateOrUpdateEstimateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
