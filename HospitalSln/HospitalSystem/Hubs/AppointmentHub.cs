using Microsoft.AspNetCore.SignalR;

namespace HospitalSystem.Hubs
{
    public class AppointmentHub : Hub
    {
        public async Task NotifyAppointmentCreated(long appointmentId, string patientName, string doctorName, DateTime appointmentDate, string department)
        {
            await Clients.All.SendAsync("AppointmentCreated", new
            {
                appointmentId,
                patientName,
                doctorName,
                appointmentDate,
                department,
                message = $"Новий запис створено: {patientName} до {doctorName} на {appointmentDate:dd.MM.yyyy HH:mm} ({department})"
            });
        }

        public async Task NotifyAppointmentUpdated(long appointmentId, string patientName, string doctorName, DateTime appointmentDate, string department)
        {
            await Clients.All.SendAsync("AppointmentUpdated", new
            {
                appointmentId,
                patientName,
                doctorName,
                appointmentDate,
                department,
                message = $"Запис перенесено: {patientName} до {doctorName} на {appointmentDate:dd.MM.yyyy HH:mm} ({department})"
            });
        }

        public async Task NotifyAppointmentDeleted(long appointmentId, string doctorName, DateTime appointmentDate)
        {
            await Clients.All.SendAsync("AppointmentDeleted", new
            {
                appointmentId,
                doctorName,
                appointmentDate,
                message = $"Година звільнена: {doctorName} на {appointmentDate:dd.MM.yyyy HH:mm} тепер доступна для запису"
            });
        }
    }
}
