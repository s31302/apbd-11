using apbd_11.DTOs;
using apbd_11.Models;

namespace apbd_11.Services;

public interface IDbService
{
    Task CreatePrescription(PrescriptionDTO prescriptionDto);
    Task<Patient> CreatePatientIfNotExist(PatientDTO dto);
    Task<Doctor?> DoctorExist(int doctorId);
    Task<List<int>> MedicamentsExist(List<int> medicamentIds);

}