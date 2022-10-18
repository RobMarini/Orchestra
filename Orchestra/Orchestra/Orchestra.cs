namespace Orchestra
{
    /// <summary>
    /// Main data structure
    /// We throw exceptions when something is disallowed because we don't have a contract to fall back to some other solution
    /// </summary>
    public class Orchestra
    {
        public Orchestra(Dictionary<Instrument, int> populationLimits)
        {
            _populationLimits = populationLimits;
            _positions = new Dictionary<Instrument, int>();
            foreach(var instrument in populationLimits.Keys)
            {
                _positions.Add(instrument, 0);
            }
        }

        private Dictionary<Instrument,int>  _populationLimits { get; set; }

        private Dictionary<Instrument,int> _positions { get; set; }

        public void AddInstrument(Instrument instrument)
        {
            if (!_populationLimits.ContainsKey(instrument))
                throw new Exception("instrument not found in orchestra");
            if (!IsPositionFilled(instrument)) 
                _positions[instrument]++;
            else if (_populationLimits[instrument] == _positions[instrument])
            {
                throw new Exception("All positions for instrument are filled");
            }
        }

        public void ApplySubstitution(SubstitutionRule substitutionRule)
        {
            if (substitutionRule == null)
                throw new ArgumentException("Substitution rule cannot be null");
            if (!ReplacementPopulationAvailable(substitutionRule))
            {
                throw new Exception("No instrument available to substitute");
            }
            if (!_populationLimits.ContainsKey(substitutionRule.ReplacingInstrument))
            {
                _populationLimits.Add(substitutionRule.ReplacingInstrument, 0);
                _positions.Add(substitutionRule.ReplacingInstrument, 0);
            }
            _populationLimits[substitutionRule.ReplacedInstrument] -= substitutionRule.ReplacedInstrumentCount;
            _populationLimits[substitutionRule.ReplacingInstrument] += substitutionRule.ReplacingInstrumentCount;
        }
        private bool ReplacementPopulationAvailable(SubstitutionRule sr)
        {
            return (
                _populationLimits.ContainsKey(sr.ReplacedInstrument) &&
                _populationLimits[sr.ReplacedInstrument] <= sr.ReplacedInstrumentCount
                );
        }
        public int RemainingPositions()
        {
            int returnVal = 0;

            foreach (var pop in _populationLimits)
            {
                returnVal += pop.Value - _positions[pop.Key];
            }
            return returnVal;

        }

        public int RemainingPositions(Instrument instrument)
        {
            if (!_populationLimits.ContainsKey(instrument))
            {
                return 0;
            }
            return _populationLimits[instrument] - _positions[instrument];
        }

        public int TotalPositions()
        {
            return _populationLimits.Select(s => s.Value).Sum();
        }
        public bool IsAllPositionsFilled()
        {
            foreach(var pop in _populationLimits)
            {
                if (_positions.ContainsKey(pop.Key))
                {
                    if (_positions[pop.Key] == pop.Value)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsPositionFilled(Instrument instrument)
        {
            if (!_populationLimits.ContainsKey(instrument))
                return true;
            return _populationLimits[instrument] == _positions[instrument];
        }
    }
}