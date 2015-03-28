using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using cloudcookerService.DataObjects;
using cloudcookerService.Models;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using Newtonsoft.Json;



namespace cloudcookerService.Controllers
{
    public class DirectUpdateController : ApiController
    {

        

        public ApiServices Services { get; set; }
        cloudcookerContext context;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new cloudcookerContext();
        }

        //getCookConfigurationForDevice will use appropriate rules to retreive the correct CookConfiguration for the device.
        //For this prototype, it simply returns the cook configuration with the highest "ActiveDate" value.
        private CookConfiguration getCookConfigurationForDevice(Device device)
        {
            CookConfiguration cookconfiguration = device.Cooks.OrderByDescending(x => x.ScheduledStart).FirstOrDefault().Configurations.OrderByDescending(x => x.activeDate).FirstOrDefault();
            return cookconfiguration;
        }

        public async Task<HttpResponseMessage> PostDeviceUpdate(DirectUpdate item)
        {
            HttpResponseMessage r = new HttpResponseMessage();

            //Validate that Device is registered and is using the correct shared secret.
            Device device = context.Devices.Include("Cooks.Configurations").Where(x => x.SerialNumber == item.SerialNumber).Where(x => x.SharedSecret == item.SharedSecret).FirstOrDefault();
            if (device == null) 
            {
                r.StatusCode = (HttpStatusCode)403;
                r.Content = new StringContent("Invalid Serial Number or Device not paired.");
                return r;
            }

            //If the Device is valid confirm that they are reporting a Valid CookConfiguration.
            CookConfiguration updateCC = await context.CookConfigurations.FindAsync(item.CookConfigurationID);
            if (updateCC != null) 
            {
                //If CookConfiguration is valid add update to database.
                var newDeviceUpdate = new DeviceUpdate {
                    Id = Guid.NewGuid().ToString("N"),
                    CookConfiguration = updateCC,
                    reportDate = DateTime.Now,
                    currentTemperature = item.currentTemperature,
                    currentFoodTemperature = item.currentFoodTemperature,
                    ControlElementActive = item.ControlElementActive
                };
                context.DeviceUpdates.Add(newDeviceUpdate);
                context.SaveChanges();
            }
            //Retrieve current CookConfiguration for Device.
            //This check happens regardless of what CookConfiguration the Device is currently reporting on.
            CookConfiguration currentCC = getCookConfigurationForDevice(device);
            if (currentCC==null)
            {
                //If no CookConfiguration is found, return status code 204, which will stop the Device.
                r.StatusCode = (HttpStatusCode)204;
                return r;
            }

            if (currentCC == updateCC)
            {
                //If the current CookConfiguration matches the one on the device. return status code 201.
                r.StatusCode = (HttpStatusCode)201;
                return r;
            }
            else
            {
                //If the current CookConfiguration is new or does not match the one on the device, 
                //create a DirectUpdateResponse and return status code 250.
                DirectUpdateResponse directupdateresponse = new DirectUpdateResponse { CookConfigurationID = currentCC.Id, setpointTemperature = currentCC.setpointTemperature, targetFoodTemperature = currentCC.targetFoodTemperature };
                r.StatusCode = (HttpStatusCode)250;
                r.Content = new StringContent(JsonConvert.SerializeObject(directupdateresponse));  
               return r;
            }

        }

    }
}
