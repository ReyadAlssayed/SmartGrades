using SmartGrades.Model;
using Supabase;
using System.Net.Http;
using System.Text.Json;

namespace SmartGrades.Services
{
    public class DataServices
    {
        private Client? _client;

        // =========================
        // Init Supabase
        // =========================
        private async Task EnsureClientAsync()
        {
            if (_client != null)
                return;

            using var stream = await FileSystem.OpenAppPackageFileAsync("appsettings.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();

            using var doc = JsonDocument.Parse(json);
            var supabase = doc.RootElement.GetProperty("Supabase");

            var url = supabase.GetProperty("Url").GetString();
            var key = supabase.GetProperty("Key").GetString();

            _client = new Client(url!, key!);
            await _client.InitializeAsync();
        }

        // =========================
        // Semesters CRUD
        // =========================
        public async Task<List<Semester>> GetSemestersAsync()
        {
            await EnsureClientAsync();

            var result = await _client!
                .From<Semester>()
                .Order("start_date", Supabase.Postgrest.Constants.Ordering.Ascending)
                .Get();

            return result.Models;
        }

        public async Task<bool> AddSemesterAsync(Semester semester)
        {
            await EnsureClientAsync();

            var response = await _client!
                .From<Semester>()
                .Insert(semester);

            return response.Models.Any();
        }

        public async Task<bool> UpdateSemesterAsync(Semester semester)
        {
            await EnsureClientAsync();

            var response = await _client!
                .From<Semester>()
                .Update(semester);

            return response.Models.Any();
        }

        public async Task<bool> DeleteSemesterAsync(Guid semesterId)
        {
            await EnsureClientAsync();

            await _client!
                .From<Semester>()
                .Where(s => s.Id == semesterId)
                .Delete();

            return true;
        }

        // =========================
        // Admin
        // =========================
      

        public async Task<bool> ChangeAdminPasswordAsync(
            Guid adminId,
            string currentPassword,
            string newPassword)
        {
            await EnsureClientAsync();

            var check = await _client!
                .From<Admin>()
                .Where(a =>
                    a.Id == adminId &&
                    a.PasswordHash == currentPassword)
                .Get();

            if (!check.Models.Any())
                return false;

            var admin = check.Models.First();
            admin.PasswordHash = newPassword;

            await _client!
                .From<Admin>()
                .Update(admin);

            return true;
        }

        // =========================
        // System Settings
        // =========================
        public async Task<SystemSettings?> GetSystemSettingsAsync()
        {
            await EnsureClientAsync();

            var result = await _client!
                .From<SystemSettings>()
                .Where(s => s.Id == 1)
                .Get();

            return result.Models.FirstOrDefault();
        }

        public async Task<string?> GetSupportPhoneAsync()
        {
            var settings = await GetSystemSettingsAsync();
            return settings?.SupportPhone;
        }

        // =========================
        // Telegram
        // =========================
        public async Task SendReportToAdminAsync(string message)
        {
            string adminChatId = "6321706551";
            string token = "8292571055:AAHnhwIYwEuA7_TgFgRKl7m6Q64khsc2UMY";

            using var http = new HttpClient();

            var url =
                $"https://api.telegram.org/bot{token}/sendMessage" +
                $"?chat_id={adminChatId}" +
                $"&text={Uri.EscapeDataString(message)}";

            await http.GetAsync(url);
        }

        // =========================
        // Admins List
        // =========================
        public async Task<List<Admin>> GetAdminsAsync()
        {
            await EnsureClientAsync();

            var result = await _client!
                .From<Admin>()
                .Order("created_at", Supabase.Postgrest.Constants.Ordering.Descending)
                .Get();

            return result.Models;
        }

        public async Task<bool> AddAdminAsync(Admin admin)
        {
            await EnsureClientAsync();

            admin.Id = Guid.NewGuid();
            admin.CreatedAt = DateTime.UtcNow;
            admin.IsActive = false; // افتراضيًا غير نشط

            var result = await _client!
                .From<Admin>()
                .Insert(admin);

            return result.Models.Any();
        }
        public async Task<bool> UpdateAdminAsync(Admin admin)
        {
            await EnsureClientAsync();

            var result = await _client!
                .From<Admin>()
                .Update(admin);

            return result.Models.Any();
        }

        public async Task<bool> DeleteAdminAsync(Guid adminId)
        {
            await EnsureClientAsync();

            await _client!
                .From<Admin>()
                .Where(a => a.Id == adminId)
                .Delete();

            return true;
        }

        public async Task<Admin?> LoginAsync(string username, string password)
        {
            await EnsureClientAsync();

            var result = await _client!
                .From<Admin>()
                .Where(a =>
                    a.Username == username &&
                    a.PasswordHash == password &&
                    a.IsActive == true)
                .Get();

            var admin = result.Models.FirstOrDefault();
            if (admin == null)
                return null;

            // تحديث آخر تسجيل دخول
            admin.LastLoginAt = DateTime.UtcNow;

            await _client!
                .From<Admin>()
                .Update(admin);

            return admin;
        }




    }
}
