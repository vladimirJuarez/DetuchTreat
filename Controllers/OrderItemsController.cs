using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    //[ApiController]
    [Route("/api/orders/{orderId}/items")]
    public class OrderItemsController: Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchRepository repository, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int orderId, int id)
        {
            try
            {
                var order = _repository.GetOrderById(orderId);
                if(order is null)
                    return NotFound();
                else
                {
                    var item = order.Items.FirstOrDefault(item => item.Id == id );
                    if(!(item is null))
                        return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                }
                    
            }
            catch (System.Exception ex)
            { 
                _logger.LogError($"Failed to get the order: {ex}", orderId);        
            }

            return BadRequest("Failed to get the order");
        }
    }
}