import { LanguageSample } from "./language-sample";

export class MultilingualSample {
  constructor(public id: number, public name: string, public languages: LanguageSample[]) { }
}
