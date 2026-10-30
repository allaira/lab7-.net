using lab7;
namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void Test_CriticalStop_TriggersAllSubscribers()
        {
            var conveyor = new ConveyorBelt();
            var master = new Master();
            var engineer = new Engineer();
            var admin = new Administration();

            int masterCalls = 0;
            int engineerCalls = 0;
            int adminCalls = 0;

            master.MessageGenerated += (m) => masterCalls++;
            engineer.MessageGenerated += (m) => engineerCalls++;
            admin.MessageGenerated += (m) => adminCalls++;

            conveyor.CriticalStop += master.OnCriticalStop;
            conveyor.CriticalStop += engineer.OnCriticalStop;
            conveyor.CriticalStop += admin.OnCriticalStop;

            conveyor.CheckWeight(120.0);

            Assert.AreEqual(1, masterCalls);
            Assert.AreEqual(1, engineerCalls);
            Assert.AreEqual(1, adminCalls);
        }

        [TestMethod]
        public void Test_MasterWorker_ImplementsThreeInterfaces()
        {
            var master = new Master();

            Assert.IsInstanceOfType(master, typeof(IStopLine));
            Assert.IsInstanceOfType(master, typeof(IDiagnose));
            Assert.IsInstanceOfType(master, typeof(IReport));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Test_NegativeWeight_ThrowsException()
        {
            var conveyor = new ConveyorBelt();
            conveyor.CurrentWeight = -10.0;
        }
    }
}
