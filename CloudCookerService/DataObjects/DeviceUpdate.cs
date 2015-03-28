using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace cloudcookerService.DataObjects
{
    /// <summary>
    /// DeviceUpdates are the updates on temperature and status provided by the devices.  Updates are associated
    /// with specific CookConfigurations.
    /// </summary>
    public class DeviceUpdate : EntityData
    {
        public string CookConfigurationID { get; set; }
        public virtual CookConfiguration CookConfiguration { get; set; }

        public int currentTemperature { get; set; }
        public int currentFoodTemperature { get; set; }
        public bool ControlElementActive { get; set; }

        public DateTime reportDate { get; set; }

    }
}