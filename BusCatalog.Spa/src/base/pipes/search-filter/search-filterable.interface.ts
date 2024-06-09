export interface SearchFilterable {
  searchTerm: string;
  onSearchTermChanges(searchTerm: string): void;
}