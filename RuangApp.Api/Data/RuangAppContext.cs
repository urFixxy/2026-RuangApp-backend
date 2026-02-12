using System;
using Microsoft.EntityFrameworkCore;
using RuangApp.Api.Models;

namespace RuangApp.Api.Data;

public class RuangAppContext(DbContextOptions<RuangAppContext> options) : DbContext(options)
{
    public DbSet<Borrowing> Borrowings => Set<Borrowing>();
    public DbSet<Rooms> Rooms => Set<Rooms>();
}
