using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly RepositoryContext ctx;

        public CategoryController(IRepositoryWrapper wrapper, RepositoryContext ctx)
        {
            this.wrapper = wrapper;
            this.ctx = ctx;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var cats = await wrapper.CategoryWrapper.GetAllCategoryAsync();

                return Ok(cats);
            }catch(Exception ex)
            {
                return StatusCode(500, "Internal serverovo error" + ex.Message);
            }
        }

        //GET: api/Category/5
        [HttpGet("{id}", Name = "")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cat = await wrapper.CategoryWrapper.GetCategoryByIdAsync(id);
                if (cat != null)
                {
                    return Ok(cat);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server issues");
            }
        }

        [HttpGet("{id}/product")]
        public async Task<IActionResult> GetCatProd(int id)
        {
            try
            {
                var cat = await wrapper.CategoryWrapper.GetCategoryWithProductAsync(id);
                if (cat != null)
                {
                    return Ok(cat);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server issues");
            }
        }

        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Check inputs");
                }

               
                var message = new CustomResponse
                {
                    ResponseID = 100,
                    ResponseCode = "00",
                    ResponseDetails = "Successful category Creation",
                    ResponseDate = DateTime.Now
                };
               
                wrapper.CategoryWrapper.CreateCategory(category);
                await wrapper.SaveAsync();

                //newProduct.
                //return Ok(message);
                return CreatedAtAction("Get", new { id = category.CatId }, message);
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                return StatusCode(500, "Internal Server error!");
            }
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateACategory(int id, [FromBody]Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Entry object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var cat = await wrapper.CategoryWrapper.GetCategoryByIdAsync(id);
                if(cat == null)
                {
                    return NotFound();
                }

                cat.CategoryName = category.CategoryName;
                wrapper.CategoryWrapper.UpdateCategory(cat);
                await wrapper.SaveAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cat = await wrapper.CategoryWrapper.GetCategoryByIdAsync(id);
                if (cat == null)
                {
                    //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (wrapper.ProductWrapper.GetProdInCat(id).Any())
                {
                    //_logger.LogError($"Cannot delete cat with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete cat. It has related accounts. Delete those accounts first");
                }

                wrapper.CategoryWrapper.DeleteCategory(cat);
                await wrapper.SaveAsync();

                return NoContent();
            }
            catch (Exception)
            {
                //_logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
