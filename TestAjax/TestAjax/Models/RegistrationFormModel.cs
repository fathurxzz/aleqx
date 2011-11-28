using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestAjax.Models
{
    public class RegistrationForm : IValidatableObject
    {
        [Required]
        public string Firstname
        {
            get;
            set;
        }

        [Required]
        public string Surname
        {
            get;
            set;
        }

        [Required]
        public int Age
        {
            get;
            set;
        }

        public string RegsitrationUniqueId
        {
            get;
            set;
        }

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Age < 21)
            {
                yield return new ValidationResult("Age must be at least 21 years.", new string[] { "Age" });
            }
        }

        #endregion
    }
}

