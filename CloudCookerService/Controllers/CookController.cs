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
    public class CookController : TableController<Cook>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            cloudcookerContext context = new cloudcookerContext();
            DomainManager = new EntityDomainManager<Cook>(context, Request, Services);
        }

        // GET tables/Cook
        public IQueryable<Cook> GetAllCook()
        {
            return Query(); 
        }

        // GET tables/Cook/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Cook> GetCook(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Cook/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Cook> PatchCook(string id, Delta<Cook> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Cook
        public async Task<IHttpActionResult> PostCook(Cook item)
        {
            Cook current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Cook/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCook(string id)
        {
             return DeleteAsync(id);
        }

    }
}