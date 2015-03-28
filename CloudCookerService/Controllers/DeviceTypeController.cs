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
    public class DeviceTypeController : TableController<DeviceType>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            cloudcookerContext context = new cloudcookerContext();
            DomainManager = new EntityDomainManager<DeviceType>(context, Request, Services);
        }

        // GET tables/DeviceType
        public IQueryable<DeviceType> GetAllDeviceType()
        {
            return Query(); 
        }

        // GET tables/DeviceType/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<DeviceType> GetDeviceType(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/DeviceType/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<DeviceType> PatchDeviceType(string id, Delta<DeviceType> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/DeviceType
        public async Task<IHttpActionResult> PostDeviceType(DeviceType item)
        {
            DeviceType current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/DeviceType/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteDeviceType(string id)
        {
             return DeleteAsync(id);
        }

    }
}