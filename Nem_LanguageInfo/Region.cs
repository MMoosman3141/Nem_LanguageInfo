using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nem_LanguageInfo {
  internal class Region {
    public string GlobalCode { get; internal set; }
    public string GlobalName { get; internal set; }

    public string RegionCode { get; internal set; }
    public string RegionName { get; internal set; }

    public string SubregionCode { get; internal set; }
    public string SubregionName { get; internal set; }

    public string IntermediateRegionCode { get; internal set; }
    public string IntermediateRegionName { get; internal set; }

    public string CountryOrArea { get; internal set; }
    public string M49Code { get; internal set; }

    public string IsoAlpha3Code { get; internal set; }
  }
}
