namespace InterviewTestMid
{

    internal interface ILogger
    {

        void WriteLogMessage(string LogMessage);
        void WriteErrorMessage(Exception Ex);
        void WriteCSVMessage(List<String> Strings);

    }

}
