export interface BackEndValidationErrors {
  type: string,
  title: string,
  status: number,
  errors: Record<string, string[]>[]
}
