import { DeferBlockBehavior, TestBed, fakeAsync } from '@angular/core/testing';

import { LinesListingComponent } from './lines-listing.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { BaseModule } from 'base/base.module';
import { Spectator, SpectatorElement, byTestId, createComponentFactory } from '@ngneat/spectator';
import { routes } from 'app/app.routes';
import { Line } from 'lines/line';
import { Router } from '@angular/router';

export const LINES = [
  new Line('107', 'Jardim Esther', 'Estação Santo André', 180, [], 1),
  new Line('148', 'Jardim Leblon', 'Terminal São Caetano', 150, [], 2),
  new Line('288', 'Terminal Ferrazópolis', 'Terminal Jabaquara', 300, [], 3),
];

describe('LinesListingComponent', () => {
  const createComponent = createComponentFactory({
    component: LinesListingComponent,
    imports: [
      BaseModule,
      RouterTestingModule.withRoutes(routes),
      HttpClientTestingModule
    ],
    deferBlockBehavior: DeferBlockBehavior.Manual
  });

  let spectator: Spectator<LinesListingComponent>;

  beforeEach(() => spectator = createComponent());

  it('should create', () => {
    expect(spectator.component).toBeTruthy();
  });

  it('should list no lines', fakeAsync(() => {
    TestBed.inject(HttpTestingController)
      .expectOne(req => !!req.url.match(/lines/))
      .flush([]);

    spectator.deferBlock().renderComplete();
    const lines = spectator.queryAll(byTestId('line-identification'));

    expect(lines.length).toBe(0);
  }));

  describe('when there are lines registered', () => {
    beforeEach(async () => {
      TestBed.inject(HttpTestingController)
        .expectOne(req => !!req.url.match(/lines/))
        .flush(LINES);
  
      await spectator.deferBlock().renderComplete();
    });
    
    it('should show a list of lines', () => {
      const lines = spectator.queryAll(byTestId('line-identification'));
  
      expect(lines.length).toBe(LINES.length);
    });

    it('when search field is populated should show only the lines that matches',
      () => {
        spectator.typeInElement('Jardim', byTestId('search-input'));
        const lines = spectator.queryAll(byTestId('line-identification'));

        expect(lines.length).toBe(2);
      });

    it('should navigate when a line is clicked', fakeAsync(() => {
      const line = spectator.queryLast(byTestId('line-identification'));
      spectator.click(line as SpectatorElement);
      spectator.tick();

      const url = spectator.inject(Router).url;
      const lastLine = LINES[LINES.length - 1];
      expect(url).toBe(`/lines/edit/${lastLine.identification}`);
    }));
  });
});
