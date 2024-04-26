﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public interface ILanguageDetectorService
    {
        LanguageSimilarityResult Detect(string sample);
        LanguageSimilarityResult Detect(int sampleId, int alphabetId, string sample);
    }
}
