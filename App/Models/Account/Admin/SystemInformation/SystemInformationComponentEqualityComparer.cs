using System.Collections.Generic;

namespace App.Models.Account.Admin.SystemInformation
{
    public class SystemInformationComponentEqualityComparer : IEqualityComparer<ISystemInformationComponent>
    {
        public bool Equals(ISystemInformationComponent x, ISystemInformationComponent y)
        {
            return x.Cardinality == y.Cardinality;
        }

        public int GetHashCode(ISystemInformationComponent obj)
        {
            return obj.Cardinality;
        }
    }
}