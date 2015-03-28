using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace cloudcookerService.DataObjects
{
    /// <summary>
    /// Cooks represent actual cooking events.  Users add cooks by setting a name, description, 
    /// a start and end time, and by assigning a device that will be used with the cook.  
    /// The user must also enter an initial CookConfiguration to inform the device what parameters to use.
    /// </summary>
    public class Cook : EntityData
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime ScheduledStart { get; set; }
        public DateTime ScheduledEnd { get; set; }

        public string FoodName { get; set; }
        public int Weight { get; set; }

        public int OutsideTemperature { get; set; }

        public string DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public string OwnerID { get; set; }
        public virtual User Owner { get; set; }

        public string imageurl { get; set; }
        public ICollection<User> Users { get; set; }

        public ICollection<CookConfiguration> Configurations { get; set; }
    }
}