using Core.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IUpdateProductService _updateProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;

        public ProductController(
            ICreateProductService createProductService,
            IUpdateProductService updateProductService,
            IDeleteProductService deleteProductService,
            IGetProductService getProductService)
        {
            _createProductService = createProductService;
            _updateProductService = updateProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
        }

        [HttpPost]
        [Route("{id:guid}/create")]
        public HttpResponseMessage Create(Guid id, [FromBody] ProductModel model)
        {
            try
            {
                var product = _createProductService.Create(id, model.Name, model.SKU, model.Description, model.Price, model.Stock, model.Tags);
                return Found(new ProductData(product));
            }
            catch (InvalidOperationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }
        }

        [HttpPost]
        [Route("{id:guid}/update")]
        public HttpResponseMessage Update(Guid id, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(id);
            if (product == null)
                return DoesNotExist();

            _updateProductService.Update(product, model.Name, model.SKU, model.Description, model.Price, model.Stock, model.Tags);
            return Found(new ProductData(product));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public HttpResponseMessage Delete(Guid id)
        {
            var product = _getProductService.GetProduct(id);
            if (product == null)
                return DoesNotExist();

            _deleteProductService.Delete(product);
            return Found();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var product = _getProductService.GetProduct(id);
            if (product == null)
                return DoesNotExist();

            return Found(new ProductData(product));
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetList(string name = null, string sku = null, decimal? minPrice = null, decimal? maxPrice = null, [FromUri] IEnumerable<string> tags = null)
        {
            var products = _getProductService.GetProducts(name, sku, minPrice, maxPrice, tags);
            return Found(products.Select(p => new ProductData(p)));
        }
    }

}
