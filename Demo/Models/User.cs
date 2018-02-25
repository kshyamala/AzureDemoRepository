using System;

namespace AzureProjectDemo.Models
{
    public class User
    {
        public User()
        {
            
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age {  get; set; }
        public string EmailId { get; set; }

    }
}
