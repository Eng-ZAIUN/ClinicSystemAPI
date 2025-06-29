using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Model
{
    [Table("Appointments")]
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }

        public DateTime Date { get; set; }

        public string Time { get; set; }

        public string Status { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }

}
