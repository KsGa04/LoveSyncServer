using LoveSyncServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoveSyncServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // 🔐 Авторизация
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || user.Password_hash != dto.Password) // Здесь лучше использовать хэш
                return Unauthorized("Неверный email или пароль");

            return Ok(user);
        }

        // 📝 Регистрация
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.users.AnyAsync(u => u.Email == dto.Email))
                return Conflict("Email уже зарегистрирован");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password_hash = dto.Password,
            };

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfile), new { id = user.Id }, user);
        }

        // 👤 Получить профиль
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var user = await _context.users
                .Include(u => u.Couple)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user == null ? NotFound() : Ok(user);
        }

        // ✏️ Обновить профиль
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateUserDto dto)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null) return NotFound();

            user.Name = dto.Name ?? user.Name;
            user.Theme = dto.Theme ?? user.Theme;
            user.Notifications_enabled = dto.NotificationsEnabled ?? user.Notifications_enabled;
            if (!string.IsNullOrEmpty(dto.Password))
                user.Password_hash = dto.Password;

            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // ❌ Удалить пользователя
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null) return NotFound();

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
