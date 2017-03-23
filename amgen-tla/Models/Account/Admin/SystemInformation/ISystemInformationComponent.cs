namespace TLA.Models.Account.Admin.SystemInformation
{
    /// <summary>
    ///  Add components to the System Information page by implementing this interface.
    ///  Multiple implementations will be discovered and incorporated.
    /// </summary>
    public interface ISystemInformationComponent
    {
        int Cardinality { get; } // determines order of display
        string Title();
        string Html();
    }
}