using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using HospitalSystem.BlazorWasm.Models;

namespace HospitalSystem.BlazorWasm.Services
{
	public class AuthService
	{
		private readonly HttpClient _httpClient;
		private readonly CustomAuthenticationStateProvider _authStateProvider;

		public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
		{
			_httpClient = httpClient;
			_authStateProvider = (CustomAuthenticationStateProvider)authStateProvider;
		}

		public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("api/Account/login", loginDto);

				if (response.IsSuccessStatusCode)
				{
					var authResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
					if (authResponse != null && !string.IsNullOrEmpty(authResponse.Token))
					{
						await _authStateProvider.MarkUserAsAuthenticated(authResponse.Token);
						return authResponse;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Login error: {ex.Message}");
			}
			return null;
		}

		public async Task<bool> RegisterAsync(RegisterDto registerDto)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("api/Account/register", registerDto);
				return response.IsSuccessStatusCode;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Register error: {ex.Message}");
				return false;
			}
		}

		public async Task LogoutAsync()
		{
			await _authStateProvider.MarkUserAsLoggedOut();
		}
	}
}
