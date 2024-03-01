using System.ComponentModel.DataAnnotations;
using System.Security;

namespace DOS_PPE.Models
{
    public class PermissionModel
    {
        [Key]
        public int ID_permis { get; set; }

        [Required]
        public string employee_id { get; set; }

        public string role_emp { get; set; }
        public DateTime Modify_date { get; set; }

    }
    public class ApermisModel
    {
        public int ID_permis { get; set; }
        public string employee_id { get; set; }
        public string name_en { get; set; }
        public string department { get; set; }
        public string emp_position { get; set; }
        public string status_active { get; set; }
        public string role_emp { get; set; }
        public string profile_pic { get; set; }
        public DateTime Modify_date { get; set; }
    }


    public class PermisViewModel
    {
        public List<EmployeeModel> Employees { get; set; }
        public List<PermissionModel> Permissions { get; set; }
        public List<ApermisModel> Apermis { get; set; }

    }

}
