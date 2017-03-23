using System;
using System.Threading;
using Elmah;
using Microsoft.Win32;
using Version = App.Models.System.Version;

namespace App.Models.Account.Admin.SystemInformation
{
    public class VersionSystemInformationComponent : ISystemInformationComponent
    {
        public int Cardinality => 0;

        public string Title()
        {
            return string.Empty;
        }

        public string Html()
        {
            try
            {
                const int gigaByte = 1048576;

                var str = "<pre>";

                str += $"\nApplication Version:       {Version.Current()}";
                str += $"\nCuture:                    {Thread.CurrentThread.CurrentCulture.Name}";
                str += $"\nUI Cuture:                 {Thread.CurrentThread.CurrentUICulture.Name}";
                str += $"\nSystem Directory:          {Environment.SystemDirectory}";
                str += $"\nMachine Name:              {Environment.MachineName}";
                str += $"\nWorkingSet:                {Environment.WorkingSet / gigaByte:N0} GB";
                str += $"\nSystem Page Size:          {Environment.SystemPageSize:N}";
                str += $"\nOS Version:                {Environment.OSVersion}";
                str += $"\nVersion:                   {Environment.Version}";
                str += $"\nUser Name:                 {Environment.UserName}";
                str += $"\nUser Domain Name:          {Environment.UserDomainName}";

                var localMachine = Registry.LocalMachine;
                var processor = localMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0");
                str += $"\nProcesser:                 {processor?.GetValue("ProcessorNameString") ?? "not found"}";
                str += $"\nProcessor Count:           {Environment.ProcessorCount}";
                str += $"\n64 Bit Operatring System:  {Environment.Is64BitOperatingSystem}";
                str += $"\n64 Bit Process:            {Environment.Is64BitProcess}";
                str += $"\n.NET Release:              {GetDotnetReleaseKeyFromRegistry()}";

                str += "</pre>";
                return str;

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return ex.Message;
            }
        }

        private static int GetDotnetReleaseKeyFromRegistry()
        {
            var releaseKey = -1;

            try
            {
                // Based on http://msdn.microsoft.com/en-us/library/hh925568%28v=vs.110%29.aspx#net_d
                using (
                  var ndpKey =
                    RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
                      .OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\"))
                {
                    if (ndpKey != null)
                    {
                        var releaseKeyValue = ndpKey.GetValue("Release") ?? "-1";
                        int.TryParse(releaseKeyValue.ToString(), out releaseKey);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return releaseKey;
        }
    }
}