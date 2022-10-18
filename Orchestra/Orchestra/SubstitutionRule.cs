using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestra
{
    /// <summary>
    /// Data structure representing one instrument being substituted for another
    /// </summary>
    public class SubstitutionRule
    {
        public SubstitutionRule(Instrument replacedInstrument, int replacedInstrumentCount, Instrument replacingInstrument, int replacingInstrumentCount)
        {
            ReplacedInstrument = replacedInstrument;
            ReplacedInstrumentCount = replacedInstrumentCount;
            ReplacingInstrument = replacingInstrument;
            ReplacingInstrumentCount = replacingInstrumentCount;
        }

        public Instrument ReplacedInstrument { get; set; }
        public int ReplacedInstrumentCount { get; set; }

        public Instrument ReplacingInstrument { get; set; }
        public int ReplacingInstrumentCount { get; set; }
    }
}
