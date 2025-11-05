using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLanguageInfo.Models {
  internal class Iso15924DataModel {
    public string Code { get; set; }
    public int Number { get; set; }
    public string EnglishName { get; set; }
    public string Alias { get; set; }
    public int Age { get; set; }
    public DateTime Date { get; set; }
  }
}
