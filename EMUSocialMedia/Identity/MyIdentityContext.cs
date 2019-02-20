using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMUSocialMedia.Identity
{

        public class MyIdentityContext : IdentityDbContext<MyIdentityUser>
        {
            public MyIdentityContext(DbContextOptions options) : base(options)
            {
                this.Database.EnsureCreated();
            }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);
            }
        }

}
