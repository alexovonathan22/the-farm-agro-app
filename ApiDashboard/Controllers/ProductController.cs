using AutoMapper;
using Contracts;
using DAL;
using DAL.Models;
using DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryWrapper wrapper;
        private readonly IMapper mapper;
        private readonly RepositoryContext ctx;

        public ProductController(IRepositoryWrapper wrapper, IMapper mapper, RepositoryContext ctx)
        {
            this.wrapper = wrapper;
            this.mapper = mapper;
            //this.ctx = ctx;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {

            try
            {
                var cats = await wrapper.ProductWrapper.GetAllProductAsync();

                return Ok(cats);
            }
            catch (Exception)
            {
                return StatusCode(500, $"Internal serverovo error");
            }
        }

        //GET: api/Product/5
        [HttpGet("{id}", Name = "product")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cat = await wrapper.ProductWrapper.GetProductByIdAsync(id);
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

        //[HttpGet("{id}/product")]
        //public async Task<IActionResult> GetCatProd(int id)
        //{
        //    try
        //    {
        //        var cat = await wrapper.ProductWrapper.GetProductWithProductAsync(id);
        //        if (cat != null)
        //        {
        //            return Ok(cat);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(500, "Internal server issues");
        //    }
        //}

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product prod)
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
                    ResponseDetails = "Successful prod Creation",
                    ResponseDate = DateTime.Now
                };
                //ctx.Prod
                wrapper.ProductWrapper.CreateProduct(prod);
                await wrapper.SaveAsync();

                //newProduct.
                //return Ok(message);
                return CreatedAtAction("GetProducts", new { id = prod.ProductId }, message);
            }
            catch (SqlException ex)
            {
                var msg = ex.Message;
                var d = ex.Data;
                var e = ex.InnerException;
                return StatusCode(500, $"Internal Server errorDB! {msg} \n=========>>oop \n {e}");
            }
            //catch(DbUpdateException ex)
            //{
            //    var msg = ex.Message;

            //    return StatusCode(500, $"Internal Server error! {msg}");

            //}
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAProduct(int id, [FromBody]Product prod)
        {
            try
            {
                if (prod == null)
                {
                    return BadRequest("Entry object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var product = await wrapper.ProductWrapper.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }



                mapper.Map(prod, product);
                //product.ProductName = prod.ProductName;
                wrapper.ProductWrapper.UpdateProduct(product);
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
                var prod = await wrapper.ProductWrapper.GetProductByIdAsync(id);
                if (prod == null)
                {
                    //_logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                //if (wrapper.ProductWrapper.GetProdInCat(id).Any())
                //{
                //    //_logger.LogError($"Cannot delete cat with id: {id}. It has related accounts. Delete those accounts first");
                //    return BadRequest("Cannot delete cat. It has related accounts. Delete those accounts first");
                //}

                wrapper.ProductWrapper.DeleteProduct(prod);
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