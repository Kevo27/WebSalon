using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebSalon.Models
{
  public class CustomerMasterDetailsModel
    {
        public Customer SelectedCustomer { get; set; }
        public string SelectedCustomerID { get; set; }

        public List<Customer> Customers { get; set; }

    }
}
