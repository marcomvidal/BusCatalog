import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlacesListingComponent } from './places-listing.component';

describe('PlacesListingComponent', () => {
  let component: PlacesListingComponent;
  let fixture: ComponentFixture<PlacesListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlacesListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PlacesListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
