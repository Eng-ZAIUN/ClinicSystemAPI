
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class PatientDto
    {
       
        public int Id { get; set; }
       
        public string FullName { get; set; } = string.Empty;
      
        public DateTime DateOfBirth { get; set; }
      
        public string Phone { get; set; } = string.Empty;
        
        public string Gender { get; set; } = string.Empty;
    }
}
