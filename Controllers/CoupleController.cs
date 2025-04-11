using LoveSyncServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoveSyncServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoupleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoupleController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Создание пары
        [HttpPost("create")]
        public async Task<IActionResult> CreateCouple([FromBody] CreateCoupleRequest request)
        {
            var user = await _context.users.FindAsync(request.UserId);
            if (user == null)
                return NotFound("Пользователь не найден");

            var code = GenerateInviteCode();

            var couple = new Couple
            {
                InviteCode = code
            };

            _context.couples.Add(couple);
            await _context.SaveChangesAsync();

            user.Couple_id = couple.Id;
            await _context.SaveChangesAsync();

            return Ok(new { coupleId = couple.Id, inviteCode = code });
        }

        // 2. Присоединение ко второй половинке
        [HttpPost("join")]
        public async Task<IActionResult> JoinCouple([FromBody] JoinCoupleRequest request)
        {
            var couple = await _context.couples.FirstOrDefaultAsync(c => c.InviteCode == request.InviteCode);

            if (couple == null)
                return BadRequest("Неверный код приглашения");

            var user = await _context.users.FindAsync(request.UserId);
            if (user == null)
                return NotFound("Пользователь не найден");

            user.Couple_id = couple.Id;
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Вы успешно присоединились к паре" });
        }

        // Генератор простого инвайт-кода
        private string GenerateInviteCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpPost("leave")]
        public async Task<IActionResult> LeaveCouple([FromBody] LeaveCoupleRequest request)
        {
            var user = await _context.users.FindAsync(request.UserId);
            if (user == null)
                return NotFound("Пользователь не найден");

            if (user.Couple_id == null)
                return BadRequest("Пользователь не состоит в паре");

            var coupleId = user.Couple_id.Value;
            user.Couple_id = null;
            await _context.SaveChangesAsync();

            // Проверим, остался ли кто-то в паре
            var remainingUsers = await _context.users
                .Where(u => u.Couple_id == coupleId)
                .ToListAsync();

            var couple = await _context.couples.FindAsync(coupleId);

            if (remainingUsers.Count == 0)
            {
                // Удалить пару, если никто не остался
                _context.couples.Remove(couple);
            }
            else
            {
                // Обновить код приглашения для оставшихся
                couple.InviteCode = GenerateInviteCode();
            }

            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Вы покинули пару" });
        }

    }

}
