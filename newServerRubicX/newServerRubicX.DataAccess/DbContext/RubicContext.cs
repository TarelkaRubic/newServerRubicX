using Microsoft.EntityFrameworkCore;
using newServerRubicX.DataAccess.Core.Interfaces.DbContext;
using newServerRubicX.DataAccess.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace newServerRubicX.DataAccess.DbContext 
{
    public class RubicContext : Microsoft.EntityFrameworkCore.DbContext, IRubicContext
    {
        public RubicContext(DbContextOptions<RubicContext> options) : base(options)
        {
        }

        public DbSet<UserRto> Users { get; set; }
        public DbSet<UserRoleRto> UserRoles { get; set; }
    }
}
