using apbd_11.Services;
using Microsoft.AspNetCore.Mvc;
using apbd_11.DTOs;

namespace apbd_11.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IDbService _dbService;

        public PrescriptionController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> PostNewPrescription([FromBody] PrescriptionDTO dto)
        {

            if (dto.Medicaments.Count == 0 || dto.Medicaments.Count > 10)
            {
                return BadRequest("RECEPTA MUSI ZAWIERAC OD 1 DO 10 LEKOW.");
            }

            if (dto.DueDate < dto.Date)
            {
                return BadRequest("DATA DUE NIE MOZE BYC WCZESNIEJSZA OD DATY WYSTAWIENIA");
            }


            var patient = await _dbService.CreatePatientIfNotExist(dto.Patient);
            


            var doctor = await _dbService.DoctorExist(dto.IdDoctor);
            if (doctor == null)
                return BadRequest("DOKTOR NIE ISTNIEJE");


            var missing = await _dbService.MedicamentsExist(dto.Medicaments.Select(m => m.IdMedicament).ToList());
            if (missing.Any())
                return BadRequest($"NIE MA LEKOW: {string.Join(", ", missing)}");


            await _dbService.CreatePrescription(dto);

            return StatusCode(201, "RECEPTA STWORZONA");
        }

    }
}