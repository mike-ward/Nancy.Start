using System.Web;

namespace TLA.Models.SystemInformation
{
    public interface ISystemInformationComponent
    {
        string Title();
        string Html();
    }
}