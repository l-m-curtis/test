using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NUnit.Framework;
using System.Text;

namespace TestProject1
{
    public class Tests
    {

        private StringWriter _CO;

        [SetUp]
        public void Setup()
        {
            this._CO = new StringWriter();
            Console.SetOut(_CO);
        }

        [TearDown()]
        public void TearDown()
        {
            this._CO.Close();
            this._CO.Dispose();
        }

        internal class MyLogger : InterviewTestMid.ILogger
        {

            public void WriteLogMessage(String Message)
            {
                Console.Write(Message);
            }

            public void WriteErrorMessage(Exception E)
            {
                Console.Write(E.Message);
            }

            public void WriteCSVMessage(List<String> Strings)
            {
                throw new NotImplementedException();
            }

            public MyLogger()
            {
            }

        }

        [Test]
        public void Mock_Test_Logger_WriteLogMessage()
        {
            MyLogger Log = new MyLogger();
            Log.WriteLogMessage("Hello!");
            String X = _CO.ToString();
            Assert.That(X == "Hello!");
        }

        [Test]
        public void Mock_Test_Logger_WriteErrorMessage()
        {
            MyLogger Log = new MyLogger();
            Log.WriteErrorMessage(new Exception("Test2"));
            String X = _CO.ToString();
            Assert.That(X == "Test2");
        }

        [Test]
        public void Mock_Test_Logger_WriteCSVMessage()
        {
            MyLogger Log = new MyLogger();
            List<String> Strings = new List<String>();
            Strings.Add("Fred");
            Assert.Throws<NotImplementedException>(() => { Log.WriteCSVMessage(Strings); });
        }


    }
}