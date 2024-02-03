import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehiclesListingComponent } from './vehicles-listing.component';

describe('VehiclesListingComponent', () => {
  let component: VehiclesListingComponent;
  let fixture: ComponentFixture<VehiclesListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VehiclesListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VehiclesListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
