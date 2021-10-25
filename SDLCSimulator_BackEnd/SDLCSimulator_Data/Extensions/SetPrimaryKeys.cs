using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SDLCSimulator_Data.Extensions
{
    public static class SetPrimaryKeys
    {
        public static void SetDbPrimaryKeys(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupTask>()
                .HasKey(gt => new {gt.GroupId,gt.TaskId});

            modelBuilder.Entity<GroupTeacher>()
                .HasKey(gt => new { gt.GroupId, gt.TeacherId });
        }
    }
}
