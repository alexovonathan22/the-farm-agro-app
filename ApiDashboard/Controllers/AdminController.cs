//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Contracts;
//using DAL.Models;
//using DAL.Models.DTO;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using AutoMapper;

//namespace ApiDashboard.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AdminController : ControllerBase
//    {
//        private readonly IRepositoryWrapper wrapper;
//        private readonly IMapper mapper;

//        public AdminController(IRepositoryWrapper wrapper, IMapper mapper)
//        {
//            this.wrapper = wrapper;
//            this.mapper = mapper;
//        }
//        // GET: api/Admin
//        [HttpGet]
//        public IEnumerable<string> Get()
//        {
//            return new string[] { "value1rtete", "value2" };
//        }

//        // GET: api/Admin/5
//        [HttpGet("{id}", Name = "product")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST: api/Admin
       

//        // PUT: api/Admin/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE: api/ApiWithActions/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
