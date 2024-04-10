export class LanguageDetectorRequest {
  constructor(
    public text: string,
    public sampleId: number = 0,
    public alphabetId: number = 0
  ) { }
}
