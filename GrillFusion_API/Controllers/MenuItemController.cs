using GrillFusion_API.Data;
using GrillFusion_API.Models;
using GrillFusion_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GrillFusion_API.Controllers
{
    [Route("api/MenuItem")]
    [ApiController]
    public class MenuItemController : Controller
    {
        //Dependency injection to use EFCore for fetching data
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;
        private readonly IWebHostEnvironment _env;
        public MenuItemController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _response = new ApiResponse();
            _env = env;
        }
        //Defining end point
        [HttpGet]
        public IActionResult GetMenuItems()
        {
            List<MenuItem> menuItems = _db.MenuItems.ToList();
            List<OrderDetail> ordDetWitRatings = _db.OrderDetails.Where(u=>u.Rating != null).ToList();

            foreach (var item in menuItems)
            {
                var ratings = ordDetWitRatings.Where(u => u.MenuItemId == item.Id).Select(u => u.Rating.Value);
                double avgRating = ratings.Any() ? ratings.Average() : 0;
                item.Ratings = avgRating;    
            }

            _response.Result = menuItems;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        //Endpoint for fetch by ID
        [HttpGet("{id:int}", Name = "GetMenuItem")]
        public IActionResult GetMenuItem(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            MenuItem? menuItem = _db.MenuItems.FirstOrDefault(u => u.Id == id);
            List<OrderDetail> ordDetWitRatings = _db.OrderDetails.Where(u => u.Rating != null && u.MenuItemId==menuItem.Id).ToList();

            var ratings = ordDetWitRatings.Select(u => u.Rating.Value);
            double avgRating = ratings.Any() ? ratings.Average() : 0;
            menuItem.Ratings = avgRating;
            


            _response.Result = menuItem;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        //Endpoint for creating menu items
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponse>> CreateMenuItem([FromForm] MenuItemCreateDTO menuItemCreateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemCreateDTO.File == null || menuItemCreateDTO.File.Length == 0)
                    {
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.IsSuccess = false;
                        _response.ErrorMessages = ["File is required"];
                        return BadRequest(_response);
                    }

                    var imagespath = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(imagespath))
                    {
                        Directory.CreateDirectory(imagespath);
                    }
                    var filePath = Path.Combine(imagespath, menuItemCreateDTO.File.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    //Lets work on uploading image now
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await menuItemCreateDTO.File.CopyToAsync(stream);
                    }

                    //Map new data to the DB
                    MenuItem menuItem = new()
                    {
                        Name = menuItemCreateDTO.Name,
                        Description = menuItemCreateDTO.Description,
                        Price = menuItemCreateDTO.Price,
                        Category = menuItemCreateDTO.Category,
                        SpecialTag = menuItemCreateDTO.SpecialTag,
                        Image = "images/" + menuItemCreateDTO.File.FileName
                    };

                    //Final step
                    _db.MenuItems.Add(menuItem);
                    await _db.SaveChangesAsync();

                    //API Final Response
                    _response.Result = menuItemCreateDTO;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetMenuItem", new { id = menuItem.Id }, _response);
                }
                else
                {
                    _response.IsSuccess = false;
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = [ex.ToString()];
            }

            return BadRequest(_response);
        }

        //Endpoint for updating menu items

        [HttpPut]
        [Consumes("multipart/form-data")]

        public async Task<ActionResult<ApiResponse>> UpdateMenuItems([FromQuery] int id, [FromForm] MenuItemUpdateDTO menuItemUpdateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemUpdateDTO == null || menuItemUpdateDTO.Id != id)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    }

                    MenuItem? obj = await _db.MenuItems.FirstOrDefaultAsync(u => u.Id == id);
                    if (obj == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return BadRequest(_response);
                    }

                    //Manually Updates all fields now

                    obj.Name = menuItemUpdateDTO.Name;
                    obj.Description = menuItemUpdateDTO.Description;
                    obj.Price = menuItemUpdateDTO.Price;
                    obj.Category = menuItemUpdateDTO.Category;
                    obj.SpecialTag = menuItemUpdateDTO.SpecialTag;

                    //Special Validation for Image
                    if (menuItemUpdateDTO.File != null && menuItemUpdateDTO.File.Length > 0)
                    {
                        var imagespath = Path.Combine(_env.WebRootPath, "images");
                        if (!Directory.Exists(imagespath))
                        {
                            Directory.CreateDirectory(imagespath);
                        }
                        var filePath = Path.Combine(imagespath, menuItemUpdateDTO.File.FileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        var filePath_OldFile = Path.Combine(_env.WebRootPath, obj.Image);
                        if (System.IO.File.Exists(filePath_OldFile))
                        {
                            System.IO.File.Delete(filePath_OldFile);
                        }

                        //Lets work on uploading image now
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await menuItemUpdateDTO.File.CopyToAsync(stream);
                        }
                        obj.Image = "images/" + menuItemUpdateDTO.File.FileName;
                    }

                    _db.MenuItems.Update(obj);
                    await _db.SaveChangesAsync();

                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }

                else
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = [ex.ToString()];
            }

            return BadRequest(_response);
        }

        //Endpoint for deleting menu items

        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> DeleteMenuItem(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    }

                    MenuItem? obj = await _db.MenuItems.FirstOrDefaultAsync(u => u.Id == id);
                    if (obj == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return BadRequest(_response);
                    }

                    //Deleting existing image first
                    var exImg = Path.Combine(_env.WebRootPath, obj.Image);
                    if (System.IO.File.Exists(exImg)) { 
                        System.IO.File.Delete(exImg);
                    }

                    //Finally, removing the entire field
                    _db.MenuItems.Remove(obj);
                    await _db.SaveChangesAsync();

                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);

                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex) 
            
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = [ex.ToString()];
            }

            return BadRequest(_response);
        }
    }
}
