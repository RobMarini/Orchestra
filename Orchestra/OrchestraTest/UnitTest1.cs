using Orchestra;
using System.Net.NetworkInformation;

namespace OrchestraTest
{
    [TestClass]
    public class UnitTest1
    {
        private Orchestra.Orchestra _testOrchestra { get; set; }
        private Orchestra.ISubstitutionRuleProvider _ruleProvider = new SubstitutionRuleProvider();
        [TestInitialize]
        public void Initialize()
        {
            _testOrchestra = new Orchestra.Orchestra(new Dictionary<Instrument, int>() { { Instrument.Cello, 2 }, { Instrument.Bass, 1 } });
        }

        [TestMethod]
        public void TestTotalPositions()
        {
            Assert.AreEqual(3, _testOrchestra.TotalPositions());
        }

        [TestMethod]
        public void TestPositionsRemaining()
        {
            Assert.AreEqual(3, _testOrchestra.RemainingPositions());
            Assert.AreEqual(2, _testOrchestra.RemainingPositions(Instrument.Cello));
            Assert.AreEqual(0, _testOrchestra.RemainingPositions(Instrument.Violin));

        }

        [TestMethod]
        public void TestIsPossitionsFilled()
        {
            Assert.IsFalse(_testOrchestra.IsPositionFilled(Instrument.Cello));
            Assert.IsTrue(_testOrchestra.IsPositionFilled(Instrument.Violin));
            Assert.IsFalse(_testOrchestra.IsAllPositionsFilled());
        }


        [TestMethod]
        public void TestAddingInstruments()
        {
            Assert.AreEqual(2, _testOrchestra.RemainingPositions(Instrument.Cello));
            _testOrchestra.AddInstrument(Instrument.Cello);
            Assert.AreEqual(1, _testOrchestra.RemainingPositions(Instrument.Cello));

            _testOrchestra.AddInstrument(Instrument.Cello);
            _testOrchestra.AddInstrument(Instrument.Bass);
            Assert.IsTrue(_testOrchestra.IsAllPositionsFilled());

        }

        [TestMethod]
        public void TestSubstitute()
        {
            Assert.AreEqual(1, _testOrchestra.RemainingPositions(Instrument.Bass));
            Assert.AreEqual(2, _testOrchestra.RemainingPositions(Instrument.Cello));
            _testOrchestra.ApplySubstitution(_ruleProvider.GetBassSubstitutions().First());
            Assert.AreEqual(4, _testOrchestra.RemainingPositions(Instrument.Cello));
            Assert.AreEqual(0, _testOrchestra.RemainingPositions(Instrument.Bass));
        }
    }
}