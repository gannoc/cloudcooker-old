using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace cloudcookerService.DataObjects
{
    /// <summary>
    /// CookConfigurations are the specific configurations used with Cooks.   In many cases, a single cook configuration
    /// can be used, but certain receipes require multiple stages (such as slow-roasting at a low temperature and adding a 
    /// crust at the end with a high temperature) or a user may want to break a cook down into different segments.   For
    /// example, when a turkey reaches a certain temperature, it may need to be basted.
    /// </summary>
    public class CookConfiguration : EntityData
    {
        public string CookID { get; set; }
        public virtual Cook Cook { get; set; }

        public int setpointTemperature { get; set; }
        public int targetFoodTemperature { get; set; }

        public DateTime activeDate { get; set; }
    }
}