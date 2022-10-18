using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestra
{
    public interface ISubstitutionRuleProvider
    {
        public List<SubstitutionRule> GetCelloSubstitution();

        public List<SubstitutionRule> GetBassSubstitutions(); 
    }

    /// <summary>
    /// This provider holds the business rules of valid SubstitutionRules
    /// Hardcoded for this exercise, this could call a DAO to store these in a db table
    /// </summary>
    public class SubstitutionRuleProvider : ISubstitutionRuleProvider
    {
        public List<SubstitutionRule> GetBassSubstitutions()
        {
            return new List<SubstitutionRule>() {
                new SubstitutionRule(Instrument.Bass,1,Instrument.Cello,2),
                new SubstitutionRule(Instrument.Bass, 1, Instrument.Violin, 4)
            };
        }

        public List<SubstitutionRule> GetCelloSubstitution()
        {
            return new List<SubstitutionRule>() {
                new SubstitutionRule(Instrument.Cello, 1, Instrument.Violin, 2)
            };
        }
    }
}
