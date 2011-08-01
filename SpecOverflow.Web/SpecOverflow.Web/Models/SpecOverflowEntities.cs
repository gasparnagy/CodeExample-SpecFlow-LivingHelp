using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SpecOverflow.Web.Models
{
    public class SpecOverflowEntities : DbContext
    {
        public DbSet<Question> Questions { get; set; }
    }
}