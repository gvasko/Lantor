import { EmptyLanguageSample } from "./empty-language-sample";

export class EmptyMultilingualSample {
  constructor(
    public id: number,
    public name: string,
    public comment: string,
    public languages: EmptyLanguageSample[])
  { }
}
