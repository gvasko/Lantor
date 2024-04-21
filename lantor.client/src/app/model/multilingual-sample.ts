import { LanguageSample } from "./language-sample";

export class MultilingualSample {
  constructor(
    public id: number,
    public name: string,
    public comment: string,
    public languages: LanguageSample[])
  { }
}
