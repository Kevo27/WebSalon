using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSalon.Models
{
    [MetadataType(typeof(CustomerMetadata))]
    public partial class Customer
    {
        public string FullName { get { return this.FName + " " + this.LName; } }
    }

    /// <summary>
    /// Metadata
    /// </summary>
    public class CustomerMetadata
    {
        [Required(ErrorMessage = "Lastname is required!")]
        [StringLength(150, ErrorMessage = "The Lastname must be between 1 and 50 characters!", MinimumLength = 1)]
        public string LName { get; set; }

        [Required(ErrorMessage = "Firstname is required!")]
        [StringLength(150, ErrorMessage = "The Firstname must be between 1 and 50 characters!", MinimumLength = 1)]
        public string FName { get; set; }
    }
}