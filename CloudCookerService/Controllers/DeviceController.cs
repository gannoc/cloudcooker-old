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
    public class DeviceController : TableController<Device>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            cloudcookerContext context = new cloudcookerContext();
            DomainManager = new EntityDomainManager<Device>(context, Request, Services);
        }

        // GET tables/Device
        public IQueryable<Device> GetAllDevice()
        {
            return Query(); 
        }

        // GET tables/Device/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Device> GetDevice(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Device/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Device> PatchDevice(string id, Delta<Device> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Device
        public async Task<IHttpActionResult> PostDevice(Device item)
        {
            Device current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Device/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteDevice(string id)
        {
             return DeleteAsync(id);
        }

    }
}