﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuickDeal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuickDealEntities : DbContext
    {
        private static QuickDealEntities _context;
        public QuickDealEntities()
            : base("name=QuickDealEntities")
        {
        }

        public static QuickDealEntities GetContext()
        {
            if ( _context == null )
            {
                _context = new QuickDealEntities();
            }
            return _context;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ad_categories> ad_categories { get; set; }
        public virtual DbSet<ad_statuses> ad_statuses { get; set; }
        public virtual DbSet<ad_types> ad_types { get; set; }
        public virtual DbSet<ad> ads { get; set; }
        public virtual DbSet<city> cities { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
