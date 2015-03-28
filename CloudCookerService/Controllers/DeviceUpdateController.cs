using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using cloudcookerService.DataObjects;
using cloudcookerService.Models;

namespace cloudcookerService.Controllers
{
    public class DeviceUpdateController : TableController<DeviceUpdate>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            cloudcookerContext context = new cloudcookerContext();
            DomainManager = new EntityDomainManager<DeviceUpdate>(context, Request, Services);
        }

        // GET tables/DeviceUpdate
        public IQueryable<DeviceUpdate> GetAllDeviceUpdate()
        {
            return Query(); 
        }

        // GET tables/DeviceUpdate/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<DeviceUpdate> GetDeviceUpdate(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/DeviceUpdate/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<DeviceUpdate> PatchDeviceUpdate(string id, Delta<DeviceUpdate> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/DeviceUpdate
        public async Task<IHttpActionResult> PostDeviceUpdate(DeviceUpdate item)
        {
            DeviceUpdate current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/DeviceUpdate/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteDeviceUpdate(string id)
        {
             return DeleteAsync(id);
        }

    }
}