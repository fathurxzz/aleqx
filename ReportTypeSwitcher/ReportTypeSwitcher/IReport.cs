namespace ReportTypeSwitcher
{
    interface IReport
    {
        string ReportType { get; }
        void Execute(string[] args);
    }
}
