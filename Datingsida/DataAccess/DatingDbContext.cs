using Datingsida.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.DataAccess
{
    public class DatingDbContext : DbContext
    {
        public DatingDbContext()
        {
        }

        public DatingDbContext(DbContextOptions<DatingDbContext> options) : base(options)
        {
        }

        public DbSet<ProfileModel> Profiles { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<FriendList> Request { get; set; }
        public DbSet<FriendList> Friendlists { get; set; }

    }
}
