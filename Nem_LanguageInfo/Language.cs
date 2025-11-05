using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nem_LanguageInfo {
  public class Language {
    public string Name {get; set;}
    public string Part1Code { get; set; }
    public string Part2TCode { get; set; }
    public string Part2BCode { get; set; }
    public string Part3Code { get; set; }
    public string Scope { get; set; }
    public string Type { get; set; }
    public string Comment { get; set; }
    public string DefaultScript { get; set; }
    public List<string> Aliases { get; set; } = [];
  
}
}
