using Supabase;
using SmartGrades.Model;
using System.Text.Json;

namespace SmartGrades.Services
{
    public class DataServices
    {
        private Client? _client;

        // =========================
        // Session (Current Teacher)
        // =========================
        public Teacher? CurrentTeacher { get; private set; }

        public bool IsLoggedIn => CurrentTeacher != null;

        public void OpenSession(Teacher teacher)
        {
            CurrentTeacher = teacher;
        }

        public void CloseSession()
        {
            CurrentTeacher = null;
        }

        public void Logout()
        {
            CurrentTeacher = null;
        }

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
        // Register - Check duplicate name
        // =========================
        public async Task<bool> TeacherNameExistsAsync(string fullName)
        {
            await EnsureClientAsync();

            var result = await _client!
                .From<Teacher>()
                .Where(t => t.FullName == fullName)
                .Get();

            return result.Models.Any();
        }

        // =========================
        // Register - Create new teacher
        // =========================
        public async Task<bool> CreateTeacherAsync(Teacher teacher)
        {
            await EnsureClientAsync();

            var response = await _client!
                .From<Teacher>()
                .Insert(teacher);

            return response.Models.Any();
        }

        // =========================
        // Login - Authenticate teacher
        // =========================
        public async Task<Teacher?> LoginAsync(string fullName, string password)
        {
            await EnsureClientAsync();

            var result = await _client!
                .From<Teacher>()
                .Where(t => t.FullName == fullName && t.Password == password)
                .Get();

            var teacher = result.Models.FirstOrDefault();

            if (teacher != null)
                OpenSession(teacher);

            return teacher;
        }

        // =========================
        // Login - Check name exists
        // =========================
        public async Task<bool> TeacherExistsAsync(string fullName)
        {
            await EnsureClientAsync();

            var result = await _client!
                .From<Teacher>()
                .Where(t => t.FullName == fullName)
                .Get();

            return result.Models.Any();
        }

        // =========================
        // Change Password ✅
        // =========================
        public async Task<bool> ChangePasswordAsync(
            string currentPassword,
            string newPassword)
        {
            await EnsureClientAsync();

            if (CurrentTeacher == null)
                return false;

            // تحقق من كلمة المرور الحالية
            var check = await _client!
                .From<Teacher>()
                .Where(t =>
                    t.Id == CurrentTeacher.Id &&
                    t.Password == currentPassword)
                .Get();

            if (!check.Models.Any())
                return false;

            // تحديث كلمة المرور
            CurrentTeacher.Password = newPassword;

            await _client!
                .From<Teacher>()
                .Update(CurrentTeacher);

            return true;
        }
    }
}
