using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;


namespace cloudcookerService.DataObjects
{
    /// <summary>
    /// DeviceTypes catagorize the types of devices available for use in the cloudcooker system.
    /// For this prototype, only one DeviceType is available and setup in the Seed.
    /// </summary>
    public class DeviceType : EntityData
    {
        public string Name { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
}