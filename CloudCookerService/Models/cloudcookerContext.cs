using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Tables;
using cloudcookerService.DataObjects;

namespace cloudcookerService.Models
{
    public class cloudcookerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
        //
        // To enable Entity Framework migrations in the cloud, please ensure that the 
        // service name, set by the 'MS_MobileServiceName' AppSettings in the local 
        // Web.config, is the same as the service name when hosted in Azure.
        private const string connectionStringName = "Name=MS_TableConnectionString";

        public cloudcookerContext() : base(connectionStringName)
        {
        }

        public DbSet<Cook> Cooks { get; set; }

        public DbSet<CookConfiguration> CookConfigurations { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<DeviceType> DeviceTypes { get; set; }

        public DbSet<DeviceUpdate> DeviceUpdates { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = ServiceSettingsDictionary.GetSchemaName();
            if (!string.IsNullOrEmpty(schema))
            {
                modelBuilder.HasDefaultSchema(schema);
            }

            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
            //We will remove cascade deletes because we have circular references.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //The following code is required to allow Devices and Cooks to have both an owner and a list of approved users that both 
            //point to the same "Users" table.

            modelBuilder.Entity<Device>()
           .HasOptional(i => i.Owner)
           .WithMany(u => u.OwnedDevices)
           .HasForeignKey(i => i.OwnerID);

            modelBuilder.Entity<Cook>()
           .HasOptional(i => i.Owner)
           .WithMany(u => u.OwnedCooks)
           .HasForeignKey(i => i.OwnerID);
        }
    }

}
