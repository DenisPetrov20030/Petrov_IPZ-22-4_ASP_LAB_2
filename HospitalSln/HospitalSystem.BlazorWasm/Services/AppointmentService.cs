using System.Net.Http.Json;
using HospitalSystem.Data.Models;
using HospitalSystem.BlazorWasm.Models;

namespace HospitalSystem.BlazorWasm.Services
{
	public class AppointmentService
	{
		private readonly HttpClient _httpClient;

		public AppointmentService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<ApiResponse<List<Appointment>>?> GetAppointmentsAsync(string? department = null, int page = 1, int pageSize = 10)
		{
			try
			{
				var url = $"api/Appointments?page={page}&pageSize={pageSize}";
				if (!string.IsNullOrEmpty(department))
				{
					url += $"&department={department}";
				}

				var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<Appointment>>>(url);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching appointments: {ex.Message}");
				return null;
			}
		}

		public async Task<Appointment?> GetAppointmentByIdAsync(long id)
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<Appointment>($"api/Appointments/{id}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching appointment: {ex.Message}");
				return null;
			}
		}

		public async Task<bool> CreateAppointmentAsync(Appointment appointment)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("api/Appointments", appointment);
				return response.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error creating appointment: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> UpdateAppointmentAsync(long id, Appointment appointment)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"api/Appointments/{id}", appointment);
				return response.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating appointment: {ex.Message}");
				return false;
			}
		}

		public async Task<bool> DeleteAppointmentAsync(long id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"api/Appointments/{id}");
				return response.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting appointment: {ex.Message}");
				return false;
			}
		}

		public async Task<List<string>?> GetDepartmentsAsync()
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<List<string>>("api/Appointments/departments");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching departments: {ex.Message}");
				return null;
			}
		}
	}
}
