using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {

        }
    }
}


//Inheriting IdentityDbContext cause it gives the features of Login/Authentication

/*
                        How To Add Migration
    0. Add Connection String into appsetting.json file
    1.Inside Models Folder create ApplcicationDbContext and Add DbSet
    2.Install tools for Microsoft Entity Core from nuget
    3.open nuget Console and fire Add-migration AddIdentityTable
        (This will create table for Identity / Users login )
    4.fire Update-Databse to push tables to Db
*/
