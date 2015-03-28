using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace cloudcookerService.DataObjects
{
    /// <summary>
    /// Users are the users of the app(s) that can log in, and view/change cook data.
    /// 
    /// Notice there is no password field.  I plan on using 3rd party authentication built into Azure Mobile Services, 
    /// so it is quite insecure for now.
    /// </summary>
    public class User : EntityData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string GoogleIdent { get; set; }
        public string imageurl { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<Device> OwnedDevices { get; set; }

        public ICollection<Cook> Cooks { get; set; }
        public ICollection<Cook> OwnedCooks { get; set; }
    }
}