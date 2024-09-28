using System.Diagnostics;

namespace InterviewTestMid
{

    internal class Logger : InterviewTestMid.ILogger
    {

        private String DQ = $"{(Char)34}";
        private String DQs = $"{(Char)34}{(Char)34}";
        private String C = $"{(Char)44}";

        public void WriteLogMessage(string LogMessage)
        {
            if (string.IsNullOrEmpty(LogMessage))
                throw new ArgumentException("Log message not provided", "LogMessage");

            Debug.WriteLine(LogMessage);
        }

        public void WriteErrorMessage(Exception Ex)
        {
            if (Ex == null)
                throw new ArgumentException("Exception not provided", "Ex");

            Debug.WriteLine($"Error recieved: {Ex.Message}");
            Debug.WriteLine($"{Ex.StackTrace}");
        }

        public void WriteCSVMessage(List<string> Strings)
        {
            if (Strings.Count > 0)
            {
                String CSVLine;

                foreach (String String in Strings)
                {
                    CSVLine = $"{DateTime.Now.ToString("yyyyMMddhhmmssfff")}{this.C}{this.DQ}{String.Replace(this.DQ, this.DQs)}{this.DQ}{System.Environment.NewLine}";
                    Debug.Write(CSVLine);
                }

            }
        }

    }
}
