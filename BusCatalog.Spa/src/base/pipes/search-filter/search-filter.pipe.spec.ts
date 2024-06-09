import { SearchFilterPipe } from "./search-filter.pipe";

describe('LinesListingComponent', () => {
  const DATA = [
    { fromwards: 'Terminal Ferrazópolis', towards: 'Terminal Piraporinha' },
    { fromwards: 'Parque Selecta', towards: 'Estação Santo André' },
    { fromwards: 'Parque Los Angeles', towards: 'Estação Santo André' },
    { fromwards: 'Jardim Cristiane', towards: 'Parque Selecta' },
  ];

  describe('when search field is empty', () => {
    it('should show all results', () => {
      const term = '';

      const results = new SearchFilterPipe().transform(DATA, term, ['fromwards']);

      expect(results.length).toBe(DATA.length);
    });
  });

  describe('when search field is populated and filterBy has one field', () => {
    const FILTER_BY = ['fromwards'];

    it('when one result matches should it', () => {
      const term = 'Terminal Ferrazópolis';

      const results = new SearchFilterPipe().transform(DATA, term, FILTER_BY);

      expect(results.length).toBe(1);
    });

    it('when more than one result matches should them', () => {
      const term = 'Parque';

      const results = new SearchFilterPipe().transform(DATA, term, FILTER_BY);

      expect(results.length).toBe(2);
    });
  });

  describe('when search field is populated and filterBy has more than one field',
    () => {
      it('when more than one result matches should them', () => {
        const filterBy = ['fromwards', 'towards'];
        const term = 'Parque Selecta';

        const results = new SearchFilterPipe().transform(DATA, term, filterBy);

        expect(results.length).toBe(2);
      });
    });
});