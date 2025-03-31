using Core.Services.Orders;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderPricingService _orderPriceService;

        public OrderController(
            ICreateOrderService createOrderService,
            IUpdateOrderService updateOrderService,
            IDeleteOrderService deleteOrderService,
            IGetOrderService getOrderService,
            IOrderRepository orderRepository,
            IOrderPricingService orderPriceService)
        {
            _createOrderService = createOrderService;
            _updateOrderService = updateOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _orderRepository = orderRepository;
            _orderPriceService = orderPriceService;
        }

        [HttpPost]
        [Route("{id:guid}/create")]
        public HttpResponseMessage Create(Guid id, [FromBody] OrderModel model)
        {
            try
            {
                var order = _createOrderService.Create(id, model.UserId, model.OrderDate, model.Products);
                return Found(new OrderData(order));
            }
            catch (InvalidOperationException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, ex.Message);
            }
        }

        [HttpPost]
        [Route("{id:guid}/update")]
        public HttpResponseMessage Update(Guid id, [FromBody] OrderModel model)
        {
            try
            {
                var order = _getOrderService.GetOrder(id);
                if (order == null)
                    return DoesNotExist();

                _updateOrderService.Update(order, model.UserId, model.OrderDate, model.Products, _orderPriceService.CalculateTotal(order.Products));
                _orderRepository.Save(order);

                return Found(new OrderData(order));
            }
            catch (ArgumentNullException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public HttpResponseMessage Delete(Guid id)
        {
            var order = _getOrderService.GetOrder(id);
            if (order == null)
                return DoesNotExist();

            _deleteOrderService.Delete(order);
            return Found();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public HttpResponseMessage GetById(Guid id)
        {
            var order = _getOrderService.GetOrder(id);
            if (order == null)
                return DoesNotExist();

            return Found(new OrderData(order));
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetList(Guid? userId = null, DateTime? fromDate = null, DateTime? toDate = null, [FromUri] IEnumerable<Guid> products = null)
        {
            var orders = _getOrderService.GetOrders(userId, fromDate, toDate, products);

            return Found(orders.Select(o => new OrderData(o)));
        }
    }

}
