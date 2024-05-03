using System.ComponentModel.DataAnnotations;

namespace HumanResourcesApi.Domain
{
    public class Employee
    {
        [Key]
        public string EmployeeID { get; set; }
       
        public string StaffIDNo { get; set; }

        public string Title { get; set; }

        public string Surname { get; set; }

        public string OtherNames { get; set; }

    }
}
