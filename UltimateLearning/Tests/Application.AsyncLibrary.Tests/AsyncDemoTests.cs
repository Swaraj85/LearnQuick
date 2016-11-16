using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.AsyncLibrary.Tests
{
    [TestClass]
    public class AsyncDemoTests
    {
        private TaskDemo _myDemo = null;

        [TestInitialize]
        public void Setup()
        {
            _myDemo = new TaskDemo();
        }

        [TestMethod]
        public void Test_Firing_Of_Task()
        {
            Assert.IsNotNull(_myDemo);
            var message = _myDemo.SimpleUsageOfTaskContinueWith(2000);
            Console.WriteLine(message);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Test_Firing_Of_Task_With_Exception()
        {
            Assert.IsNotNull(_myDemo);
            var message = _myDemo.SimpleUsageOfTaskContinueWithAndException(2000, (s) =>
            {
                Console.WriteLine("Executing: {0}", s);
                throw new UnauthorizedAccessException("Not allowed to access the service");
            });

            Console.WriteLine(message);
        }
    }
}
