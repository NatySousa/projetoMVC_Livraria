using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projeto_Estudo_MVC.Entities;

namespace Projeto_Estudo_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Livro> Livros {get; set;}
     
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
