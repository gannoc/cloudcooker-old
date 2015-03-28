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
    public class CookConfigurationController : TableController<CookConfiguration>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            cloudcookerContext context = new cloudcookerContext();
            DomainManager = new EntityDomainManager<CookConfiguration>(context, Request, Services);
        }

        // GET tables/CookConfiguration
        public IQueryable<CookConfiguration> GetAllCookConfiguration()
        {
            return Query(); 
        }

        // GET tables/CookConfiguration/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<CookConfiguration> GetCookConfiguration(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/CookConfiguration/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<CookConfiguration> PatchCookConfiguration(string id, Delta<CookConfiguration> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/CookConfiguration
        public async Task<IHttpActionResult> PostCookConfiguration(CookConfiguration item)
        {
            CookConfiguration current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/CookConfiguration/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCookConfiguration(string id)
        {
             return DeleteAsync(id);
        }

    }
}