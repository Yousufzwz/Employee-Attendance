using Microsoft.EntityFrameworkCore;
using RectatSystem.Models;

namespace RectatSystem.Data;

public class TelegramBotContext : DbContext
{
    public TelegramBotContext(DbContextOptions<TelegramBotContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}