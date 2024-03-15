using Aplikace.Models;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.EntityFrameworkCore.Firebird;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Security.Cryptography;

namespace Aplikace.Databaze
{
    public class PropojeniDB : DbContext, IDbContext
    {
        public PropojeniDB(DbContextOptions<PropojeniDB> options) : base(options)
        {
        }

        public DbSet<Pot> Pot { get; set; }
        public DbSet<Lid> Lid { get; set; }

    }
}
