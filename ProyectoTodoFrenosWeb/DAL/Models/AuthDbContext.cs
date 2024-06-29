using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext>options): base(options){ }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuiler)
    {
        base.OnConfiguring(optionsBuiler);
        optionsBuiler.UseSqlServer();
    }
}
