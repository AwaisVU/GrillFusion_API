using GrillFusion_API.Data;
using GrillFusion_API.Models;
using GrillFusion_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GrillFusion_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : Controller
    {

        //DIs for DbContext and APIresponse
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;

        //Constructor
        public OrderDetailController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        //Define Update Endpoint
        [HttpPut("{orderDetailId:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateOrderDetail(int orderDetailId, [FromBody] OrderDetailUpdateDTO orderDetailDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(orderDetailId != orderDetailDTO.OrderDetailId)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("Invalid Order ID");
                        return BadRequest(_response);
                    }

                    OrderDetail? orDetFromDb = _db.OrderDetails.FirstOrDefault(u => u.OrderDetailId == orderDetailId);
                    if (orDetFromDb == null)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorMessages.Add("Order Not Found");
                        return BadRequest(_response);
                    }

                    //Finally, only update the rating
                    orDetFromDb.Rating = orderDetailDTO.Rating;
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
