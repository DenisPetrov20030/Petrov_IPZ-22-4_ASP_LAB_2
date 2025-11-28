using System.Net.Http.Json;
using HospitalSystem.Data.Models;

namespace HospitalSystem.BlazorWasm.Services
{
	public class DoctorService
	{
		private readonly HttpClient _httpClient;

		public DoctorService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<Doctor>?> GetDoctorsAsync()
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<List<Doctor>>("api/Doctors");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching doctors: {ex.Message}");
				return null;
			}
		}

		public async Task<Doctor?> GetDoctorByIdAsync(long id)
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<Doctor>($"api/Doctors/{id}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching doctor: {ex.Message}");
				return null;
			}
		}

		public async Task<bool> CreateDoctorAsync(Doctor doctor)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("api/Doctors", doctor);
				return response.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error creating doctor: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> UpdateDoctorAsync(long id, Doctor doctor)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"api/Doctors/{id}", doctor);
				return response.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating doctor: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> DeleteDoctorAsync(long id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"api/Doctors/{id}");
				return response.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting doctor: {ex.Message}");
				return false;
			}
		}
	}
}
