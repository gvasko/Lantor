export class LanguageSimilarityValue {
  constructor(public name: string, public value: number) {

  }
}

export class LanguageSimilarityResult {
  similarityValues: Array<LanguageSimilarityValue> = [];
  durationMillisec: number = 0;
  significantCount: number = 0;
}
