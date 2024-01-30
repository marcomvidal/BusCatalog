import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LinesListingComponent } from './lines-listing.component';

describe('LinesListingComponent', () => {
  let component: LinesListingComponent;
  let fixture: ComponentFixture<LinesListingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LinesListingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LinesListingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
