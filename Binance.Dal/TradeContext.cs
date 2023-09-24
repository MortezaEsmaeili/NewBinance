using Binance.Dal.Model;
using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Runtime.CompilerServices;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Reflection;

namespace Binance.Dal
{
    public class TradeContext : DbContext
    {
        static private string s_migrationSqlitePath;

        
        public TradeContext() : base("TradeDB")
        { }
        public TradeContext(DbConnection connection) : base(connection, true)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var model = modelBuilder.Build(Database.Connection);
            ISqlGenerator sqlGenerator = new SqliteSqlGenerator();
            string sql = sqlGenerator.Generate(model.StoreModel);            
        }
    
        public bool Migrate()
        {
            try
            {
                var internalContext = this.GetType().GetProperty("InternalContext", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
                var providerName = (string)internalContext.GetType().GetProperty("ProviderName").GetValue(internalContext);
                var configuration = new Binance.Dal.Migrations.Configuration()
                {
                    TargetDatabase = new DbConnectionInfo(this.Database.Connection.ConnectionString, providerName)
                };
                var migrator = new DbMigrator(configuration);


                // Migration
                migrator.Update();
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public DbSet<TradeBotRange> TradeBotRanges { get; set; }
        public DbSet<TradingData> TradingDatas { get; set; }

    }
}
