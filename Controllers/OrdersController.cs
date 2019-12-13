using System;
using System.Collections.Generic;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController: Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<Order> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IDutchRepository repository, ILogger<Order> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                _logger.LogInformation("Getting all orders");
                var results = _repository.GetAllOrders(includeItems);
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to get orders: {ex}");        
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if(order is null)
                    return NotFound();
                else
                {
                    _logger.LogInformation($"Getting the next orderId: {id}");
                    return Ok(_mapper.Map<Order, OrderViewModel>(order));
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get the order: {ex}");
                return BadRequest("Failed to get the order");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            // add it to the db
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newOrder = _mapper.Map<OrderViewModel, Order>(model);

                if (newOrder.OrderDate == DateTime.MinValue)
                {
                    newOrder.OrderDate = DateTime.Now;
                }

                _repository.AddEntity(newOrder);
                if (_repository.SaveAll())
                {
                    return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new order: {ex}");
            }

            return BadRequest("Failed to save new order");
        }
    }
}