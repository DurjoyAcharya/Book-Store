using BookStore.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.Domain;

public class DatabaseContext(DbContextOptions<DatabaseContext> mssql) : 
    IdentityDbContext<ApplicationUsers>(mssql);