﻿namespace Lantor.DomainModel
{
    public class LanguageSample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sample { get; set; }
        public int MultilingualSampleId { get; set; }

        public LanguageSample(): this("", "")
        {
        }

        public LanguageSample(string name, string sample)
        {
            Name = name;
            Sample = sample;
        }
    }
}
