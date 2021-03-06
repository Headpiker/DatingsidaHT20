﻿using Datingsida.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datingsida.DataAccess
{
    public class DatingDbContext : DbContext
    {
        public DatingDbContext(DbContextOptions<DatingDbContext> options) : base(options)
        {
        }

        public DbSet<ProfileModel> Profiles { get; set; }
    }
}
