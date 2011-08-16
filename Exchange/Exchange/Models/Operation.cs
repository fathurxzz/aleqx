using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class Oper : IEquatable<Oper>, IOperationFilter
    {
        public EOperation Operation { get; set; }
        public string OperName{get; set;}
        public bool Selected { get; set; }

        public bool Equals(Oper other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return Operation.Equals(other.Operation) && OperName.Equals(other.OperName);
        }

        public override int GetHashCode()
        {
            int hashCurrencyCurId = Operation.GetHashCode();
            int hashCurrencyCurDef = OperName == null ? 0 : OperName.GetHashCode();
            return hashCurrencyCurId ^ hashCurrencyCurDef;
        }
    }
}