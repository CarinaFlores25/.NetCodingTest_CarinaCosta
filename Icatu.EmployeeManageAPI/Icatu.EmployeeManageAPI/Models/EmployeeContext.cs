using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Icatu.EmployeeManageAPI.Models
{
    public class EmployeeContext: DbContext
    {
     
        public EmployeeContext(DbContextOptions<EmployeeContext>options)
            : base(options) { }

        public DbSet<EmployeeItem> Employees { get; set; }
    }
}
