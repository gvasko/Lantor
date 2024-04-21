using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lantor.DomainModel
{
    public class MultilingualSample
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public List<LanguageSample> Languages { get; set; }

        public MultilingualSample()
        {
            Name = "";
            Comment = "";
            Languages = [];
        }

        public MultilingualSample(string name, string comment) 
        {
            Name = name;
            Comment = comment;
            Languages = [];
        }
    }
}
