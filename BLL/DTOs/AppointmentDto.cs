
namespace BLL.DTOs
{
   
        public class AppointmentDto
        {
            public int Id { get; set; }
            public int PatientId { get; set; }
            public DateTime Date { get; set; }
            public string Time { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
        }
    

}
