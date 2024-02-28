using System.ComponentModel.DataAnnotations;

namespace DOS_PPE.Models
{
    public class EmployeeModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string employee_id { get; set; }
        public string perfix_en { get; set; }
        public string perfix_th { get; set; }
        public string name_th { get; set; }
        public string name_en { get; set; }
        public string name_nick_en { get; set; }
        public string name_nick_th { get; set; }
        public DateTime entry_date { get; set; }
        public DateTime birth_date { get; set; }
        public string emp_position { get; set; }
        public string nationality { get; set; }
        public string blood_group { get; set; }
        public string phone_number { get; set; }
        public string gender { get; set; }
        public string education { get; set; }
        public string academy { get; set; }
        public string department { get; set; }
        public string status_active { get; set; }
        public string signature_pic { get; set; }
        public string email { get; set; }
        public string hod { get; set; }
        public string profile_pic { get; set; }
        public string address_no { get; set; }
        public string addreess_district { get; set; }
        public string addreess_sub_district { get; set; }
        public string addreess_province { get; set; }
    }
}
