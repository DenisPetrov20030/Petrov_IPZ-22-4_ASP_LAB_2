using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospitalSystem.Data.Models;
using HospitalSystem.Data.Repositories;

namespace HospitalSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DoctorsController : ControllerBase
	{
		private readonly IHospitalRepository _repository;

		public DoctorsController(IHospitalRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var doctors = _repository.Doctors.OrderBy(d => d.FullName).ToList();
			return Ok(doctors);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(long id)
		{
			var doctor = _repository.GetDoctorById(id);
			if (doctor == null)
			{
				return NotFound(new { message = "Лікаря не знайдено" });
			}
			return Ok(doctor);
		}

		[HttpPost]
		[Authorize(Roles = "Doctor,Admin")]
		public IActionResult Create([FromBody] Doctor doctor)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_repository.CreateDoctor(doctor);
			return CreatedAtAction(nameof(GetById), new { id = doctor.DoctorID }, doctor);
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Doctor,Admin")]
		public IActionResult Update(long id, [FromBody] Doctor doctor)
		{
			if (id != doctor.DoctorID)
			{
				return BadRequest(new { message = "ID не співпадає" });
			}

			var existing = _repository.GetDoctorById(id);
			if (existing == null)
			{
				return NotFound(new { message = "Лікаря не знайдено" });
			}

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_repository.UpdateDoctor(doctor);
			return NoContent();
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(long id)
		{
			var doctor = _repository.GetDoctorById(id);
			if (doctor == null)
			{
				return NotFound(new { message = "Лікаря не знайдено" });
			}

			_repository.DeleteDoctor(doctor);
			return NoContent();
		}
	}
}
