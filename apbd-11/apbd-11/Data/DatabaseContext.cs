using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using apbd_11.Models;

namespace apbd_11.Data;

public class DatabaseContext : DbContext
{

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    

    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    
}
