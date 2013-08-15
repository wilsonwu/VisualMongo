using VisualMongo.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VisualMongo.Entity;

namespace VisualMongo.Test
{


    /// <summary>
    ///This is a test class for ConnectionServiceTest and is intended
    ///to contain all ConnectionServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConnectionServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for UpdateConnection
        ///</summary>
        [TestMethod()]
        public void UpdateConnectionTest()
        {
            string connectionNameOld = "local"; // TODO: Initialize to an appropriate value
            string connectionNameNew = "local" + new Random().Next(1000, 9999).ToString(); // TODO: Initialize to an appropriate value
            string serverName = string.Empty; // TODO: Initialize to an appropriate value
            int serverPort = 0; // TODO: Initialize to an appropriate value
            string userName = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            string databases = string.Empty; // TODO: Initialize to an appropriate value
            ConnectionEntity expected = null; // TODO: Initialize to an appropriate value
            ConnectionEntity actual;
            actual = ConnectionService.UpdateConnection(connectionNameOld, connectionNameNew, serverName, serverPort, userName, password, databases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteConnection
        ///</summary>
        [TestMethod()]
        public void DeleteConnectionTest()
        {
            string connectionName = "local6466"; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = ConnectionService.DeleteConnection(connectionName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
