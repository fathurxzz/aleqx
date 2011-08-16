using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class OperationSign : IEquatable<OperationSign>, IOperSignFilter
    {
        public EOperSign OperSign { get; set; }
        public string OperSignName { get; set; }
        public bool Selected { get; set; }

        public bool Equals(OperationSign other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return OperSign.Equals(other.OperSign) && OperSignName.Equals(other.OperSignName);
        }

        public override int GetHashCode()
        {
            int hashCurrencyCurId = OperSign.GetHashCode();
            int hashCurrencyCurDef = OperSignName == null ? 0 : OperSignName.GetHashCode();
            return hashCurrencyCurId ^ hashCurrencyCurDef;
        }
    }
}