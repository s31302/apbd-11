using apbd_11.Data;
using apbd_11.DTOs;
using apbd_11.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_11.Services;

public class DbService: IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    
    public async Task CreatePrescription(PrescriptionDTO prescriptionDto)
    {
        var prescription = new Prescription
        {
            Date = prescriptionDto.Date,
            DueDate = prescriptionDto.DueDate,
            IdPatient = prescriptionDto.Patient.IdPatient,
            IdDoctor = prescriptionDto.IdDoctor
        };

        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();

        foreach (var medicamentDto in prescriptionDto.Medicaments)
        {
            var prescriptionMedicament = new PrescriptionMedicament
            {
                IdMedicament = medicamentDto.IdMedicament,
                IdPrescription = prescription.IdPrescription,
                Dose = medicamentDto.Dose,
                Details = medicamentDto.Description
            };
            _context.PrescriptionMedicaments.Add(prescriptionMedicament);
        }

        await _context.SaveChangesAsync();
    }
    
    public async Task<Patient> CreatePatientIfNotExist(PatientDTO dto)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == dto.IdPatient);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Birthdate = dto.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        return patient;
    }

    public async Task<Doctor?> DoctorExist(int doctorId)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == doctorId);
    }

    public async Task<List<int>> MedicamentsExist(List<int> medicamentIds)
    {
        var existing = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        return medicamentIds.Except(existing).ToList();
    }
    
}

