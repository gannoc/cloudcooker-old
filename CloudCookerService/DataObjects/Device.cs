using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace cloudcookerService.DataObjects
{
    /// <summary>
    /// A Device is a PID controller or other monitor that acts as an IoT device to report information about the smoker, oven, sous vide, etc.
    /// </summary>
    public class Device : EntityData
    {

        public string SerialNumber { get; set; }
        public string SharedSecret { get; set; }
        public string Name { get; set; }

        public string DeviceTypeID { get; set; }
        public virtual DeviceType DeviceType { get; set; }

        public string OwnerID { get; set; }
        public virtual User Owner { get; set; }

        public string imageurl { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Cook> Cooks { get; set; }
    }

}