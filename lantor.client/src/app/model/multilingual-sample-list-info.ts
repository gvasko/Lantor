export class MultilingualSampleListInfo {
  constructor(
    public id: number,
    public name: string,
    public comment: string,
    public ownerId: number,
    public languageCount: number
  ) { }
}
