using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class Status : IEquatable<Status>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }


        public bool Equals(Status other)
        {
            if (Object.ReferenceEquals(other, null)) return false;

            if (Object.ReferenceEquals(this, other)) return true;

            return Id.Equals(other.Id) && Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            int hashStatusId = Id.GetHashCode();

            // Get the hash code for the CurDef field if it is not null.
            int hashStatusName = Name == null ? 0 : Name.GetHashCode();


            // Calculate the hash code for the currency.
            return hashStatusId ^ hashStatusName;
        }
    }


}