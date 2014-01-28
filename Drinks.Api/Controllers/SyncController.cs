using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Drinks.Api.Entities;
using Drinks.Entities.Extensions;
using Drinks.Services;
using JetBrains.Annotations;

namespace Drinks.Api.Controllers
{
    public class SyncController : ApiController
    {
        const string Header = "Selectionnez...";

        readonly IProductsService _productsService;

        public SyncController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public JsonResult<SyncResponse> Get()
        {
            var products = GenerateProducts();
            var response = new SyncResponse(Header, products, DateTime.Now.ToUnixTimestamp());
            return Json(response);
        }

        [NotNull]
        string[] GenerateProducts()
        {
            return _productsService.GetAllProducts().OrderBy(x => x.Id).Select(x => x.ToString()).ToArray();
        }
    }
}