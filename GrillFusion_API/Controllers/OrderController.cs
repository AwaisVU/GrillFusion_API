﻿using GrillFusion_API.Data;
using GrillFusion_API.Models;
using GrillFusion_API.Models.Dto;
using GrillFusion_API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GrillFusion_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrderController : Controller
    {
        //Dependency Injection
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;

        //Define Constructor?
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        //Define Endpoints

        //1. Get all orders list for a user
        [HttpGet]
        public ActionResult<ApiResponse> GetOrders(string userId = "")
        {
            IEnumerable<Order> orderList = _db.Orders.Include(u => u.OrderDetails).ThenInclude(u => u.MenuItem).OrderByDescending(u => u.OrderId);
            if (!string.IsNullOrEmpty(userId))
            {
                orderList=orderList.Where(u=>u.ApplicationUserId == userId);
            }

            _response.Result = orderList;
            _response.StatusCode=HttpStatusCode.OK;
            return Ok(_response);
        }

        //2. Get individual order by its specific ID
        [HttpGet("{orderId:int}")]
        public ActionResult<ApiResponse> GetOrder(int orderId)
        {
            if (orderId == 0)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.IsSuccess=false;
                _response.ErrorMessages.Add("Invalid Order ID");
                return BadRequest(_response);
            }

            Order? order = _db.Orders.Include(u => u.OrderDetails).ThenInclude(u => u.MenuItem).FirstOrDefault(u => u.OrderId==orderId);

            if(order == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Order Not Found");
                return BadRequest(_response);
            }

            _response.Result = order;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        //3. Create Order EndPoint
        [HttpPost]

        public async Task<ActionResult<ApiResponse>> CreateOrder([FromBody] OrderCreateDTO orderDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Order order = new()
                    {
                        PickUpName = orderDTO.PickUpName,
                        PickUpPhoneNo = orderDTO.PickUpPhoneNo,
                        PickUpEmail = orderDTO.PickUpEmail,
                        OrderDate = DateTime.UtcNow,
                        OrderTotal = orderDTO.OrderTotal,
                        Status = SD.status_confirmed,
                        TotalItem = orderDTO.TotalItem,
                        ApplicationUserId = orderDTO.ApplicationUserId
                    };

                    _db.Orders.Add(order);
                    await _db.SaveChangesAsync();

                    foreach(var orderDetailDTO in orderDTO.OrderDetailDTO)
                    {
                        OrderDetail orderDetail = new()
                        {
                            OrderId = order.OrderId,
                            MenuItemId = orderDetailDTO.MenuItemId,
                            Quantity = orderDetailDTO.Quantity,
                            ItemName = orderDetailDTO.ItemName,
                            Price = orderDetailDTO.Price,
                        };

                        _db.OrderDetails.Add(orderDetail);
                    }
                    _db.SaveChanges();
                    _response.Result = order;
                    order.OrderDetails = [];
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtAction(nameof(GetOrder), new {orderId = order.OrderId}, _response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = ModelState.Values.SelectMany(u=>u.Errors).Select(u=>u.ErrorMessage).ToList();
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }

        //3. Update Order Endpoint

        ///////////////////////////////////////////////////////////////
        ///

        [HttpPut("{orderId:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateOrder(int orderId, [FromBody] OrderUpdateDTO orderDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(orderId != orderDTO.OrderId)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("Order ID is invalid");
                        return BadRequest(_response);
                    }

                    Order? orderFromDb = _db.Orders.FirstOrDefault(o => o.OrderId == orderId);

                    if(orderFromDb == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("Order Not Found");
                        return BadRequest(_response);
                    }
                    //Validations of data items
                    if (!string.IsNullOrEmpty(orderDTO.PickUpName))
                    {
                        orderFromDb.PickUpName = orderDTO.PickUpName;
                    }
                    if (!string.IsNullOrEmpty(orderDTO.PickUpPhoneNo))
                    {
                        orderFromDb.PickUpPhoneNo = orderDTO.PickUpPhoneNo;
                    }
                    if (!string.IsNullOrEmpty(orderDTO.PickUpEmail))
                    {
                        orderFromDb.PickUpEmail = orderDTO.PickUpEmail;
                    }
                    if (!string.IsNullOrEmpty(orderDTO.Status))
                    {
                        var status = orderDTO.Status.Trim().ToLower();
                        
                        if(orderFromDb.Status.Equals(SD.status_confirmed,StringComparison.InvariantCultureIgnoreCase) && status.Equals(SD.status_readyForPickup, StringComparison.InvariantCultureIgnoreCase))
                        {
                            orderFromDb.Status = SD.status_readyForPickup;
                        }
                        if (orderFromDb.Status.Equals(SD.status_readyForPickup, StringComparison.InvariantCultureIgnoreCase) && status.Equals(SD.status_completed, StringComparison.InvariantCultureIgnoreCase))
                        {
                            orderFromDb.Status = SD.status_completed;
                        }
                        if(status.Equals(SD.status_cancelled, StringComparison.InvariantCultureIgnoreCase) && !orderFromDb.Status.Equals(SD.status_completed, StringComparison.InvariantCultureIgnoreCase))
                        {
                            orderFromDb.Status = SD.status_cancelled;
                        }
                    }

                    await _db.SaveChangesAsync();

                   
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = ModelState.Values.SelectMany(u => u.Errors).Select(u => u.ErrorMessage).ToList();
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }
        }
    }
}
