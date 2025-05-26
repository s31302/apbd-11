using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_11.Models;

[Table("Prescription")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    public DateTime Date { get; set; }
    
    public DateTime DueDate { get; set; }
    
    [ForeignKey(nameof(Doctor))]
    public int IdDoctor { get; set; }
    
    [ForeignKey(nameof(Patient))]
    public int IdPatient { get; set; }
    
}