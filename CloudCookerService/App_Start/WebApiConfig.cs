using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.AppService.Config;
using cloudcookerService.DataObjects;
using cloudcookerService.Models;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;



namespace cloudcookerService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            AppServiceExtensionConfig.Initialize();
            
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            //This configuration is required for the Azure Mobile Services test web site to render JSON examples with nested objects.
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            Database.SetInitializer(new cloudcookerInitializer());
        }
    }

    public class cloudcookerInitializer : ClearDatabaseSchemaIfModelChanges<cloudcookerContext>
    {
        protected override void Seed(cloudcookerContext context)
        {

            //Add users to the system.  Email addresses as distinct per user, and this does not include any 3rd party identity service integration.  
            var Users = new List<User>
            {
                 new User { Id = Guid.NewGuid().ToString("N"), FirstName = "John",   LastName = "Smith", Email="xxxxx@gmail.com" },
                 new User { Id = Guid.NewGuid().ToString("N"), FirstName = "Test1",   LastName = "User1", Email="test1@cookercorp.com" },
                 new User { Id = Guid.NewGuid().ToString("N"), FirstName = "Test2",   LastName = "User2", Email="test3@cookercorp.com" }
            };
            Users.ForEach(u => context.Users.AddOrUpdate(x => x.Email, u));

            //Add a single device type.  Different devices have different capabilities that ned to be recognized by the system.
            DeviceType TestDeviceType = new DeviceType { Id = Guid.NewGuid().ToString("N"), Name = "Arduino Prototype 1" };

            //Add three devices with different owners and access rights.
            Device BGEDevice = new Device { Id = Guid.NewGuid().ToString("N"), Name = "Big Green Egg Controller", SerialNumber = "12345ABCDE12345", SharedSecret = "2dd2ae03e96642b0b17743cd4d757f51", Owner = Users[0], DeviceType = TestDeviceType };
            BGEDevice.Users = new List<User> { };
            BGEDevice.Users.Add(Users[0]);

            Device Dummy1 = new Device { Id = Guid.NewGuid().ToString("N"), Name = "Dummy Device 1", SerialNumber = "111111111111111", SharedSecret = "2cc788bc373b48cc8d804475bc8c9fa1", Owner = Users[0], DeviceType = TestDeviceType };
            Dummy1.Users = new List<User> { };
            Dummy1.Users.Add(Users[0]);
            Dummy1.Users.Add(Users[1]);

            Device Dummy2 = new Device { Id = Guid.NewGuid().ToString("N"), Name = "Dummy Device 2", SerialNumber = "222222222222222", SharedSecret = "140a627f208740d98a8f36c1d0b9fb26", Owner = Users[1], DeviceType = TestDeviceType };
            Dummy2.Users = new List<User> { };
            Dummy2.Users.Add(Users[1]);
            Dummy2.Users.Add(Users[2]);

            context.Devices.AddOrUpdate(x => x.SerialNumber, BGEDevice);
            context.Devices.AddOrUpdate(x => x.SerialNumber, Dummy1);
            context.Devices.AddOrUpdate(x => x.SerialNumber, Dummy2);

            //Add a Cook entity for testing.
            Cook SeedCook = new Cook
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Test Cook",
                Description = "This is a test cook for UI and Device testing.",
                FoodName = "Whole Chicken"
                ,
                Weight = 160,
                OutsideTemperature = 32,
                Owner = Users[0],
                Device = BGEDevice,
                ScheduledStart = DateTime.Parse("2/28/2015 3:00:00 PM"),
                ScheduledEnd = DateTime.Parse("2/28/2015 6:00:00 PM")
            };

            //Add users to the testing Cook
            SeedCook.Users = new List<User> { };
            SeedCook.Users.Add(Users[0]);
            SeedCook.Users.Add(Users[1]);
            SeedCook.Users.Add(Users[2]);

            context.Cooks.AddOrUpdate(x => x.Name, SeedCook);


            //Adds an initial CookConfiguration and some DeviceUpdates for testing purposes.
            CookConfiguration SeedConfiguration = new CookConfiguration { Id = Guid.NewGuid().ToString("N"), Cook = SeedCook, setpointTemperature = 2400, targetFoodTemperature = 1650, activeDate = DateTime.Parse("2/28/2015 3:00:00 PM") };
            context.CookConfigurations.AddOrUpdate(x => x.activeDate, SeedConfiguration);

            var TestDeviceUpdates = new List<DeviceUpdate>
            {
                 new DeviceUpdate { Id = Guid.NewGuid().ToString("N"), CookConfiguration=SeedConfiguration,  reportDate =  DateTime.Parse("2/28/2015 3:00:08 PM"), currentTemperature=846, currentFoodTemperature=332, ControlElementActive=true },
                 new DeviceUpdate { Id = Guid.NewGuid().ToString("N"), CookConfiguration=SeedConfiguration,  reportDate =  DateTime.Parse("2/28/2015 3:00:22 PM"), currentTemperature=872, currentFoodTemperature=322, ControlElementActive=true },
                 new DeviceUpdate { Id = Guid.NewGuid().ToString("N"), CookConfiguration=SeedConfiguration,  reportDate =  DateTime.Parse("2/28/2015 3:00:54 PM"), currentTemperature=905, currentFoodTemperature=377, ControlElementActive=true },
            };
            TestDeviceUpdates.ForEach(u => context.DeviceUpdates.AddOrUpdate(x => x.Id, u));

            /*
            //Catching the exception as a DbEntityValidationException will provide more information if there are issues with the Enitiy Model.
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw (dbEx);
            }
            */
            base.Seed(context);
        }
    }
}

