using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cloudcookerService.DataObjects
{
    /// <summary>
    /// DirectUpdates are the Data Transfer Objects used by the Devices to send updates to the mobile service.
    /// </summary>
    public class DirectUpdate
    {
        public string SerialNumber { get; set; }
        public string SharedSecret { get; set; }
        public string CookConfigurationID { get; set; }
        public int currentTemperature { get; set; }
        public int currentFoodTemperature { get; set; }
        public bool ControlElementActive { get; set; }

    }
    /// <summary>
    /// DirectUpdateResponses are the Data Transfer Objects used by the mobile service to send updates to the device.
    /// </summary>
    public class DirectUpdateResponse
    {
        public string CookConfigurationID { get; set; }
        public int setpointTemperature { get; set; }
        public int targetFoodTemperature { get; set; }

    }
}