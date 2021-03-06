﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DSCC_CW1_5591.Repositories;
using DSCC_CW1_5591.Models;
using System.Transactions;

namespace DSCC_CW1_5591.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //GET api/values
        /// <summary>
        /// This method returns information for array of products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productRepository.GetProducts();
            return new OkObjectResult(products);

        }

        //GET api/values/5
        /// <summary>
        /// This method returns information about specific product which is passed with ID 
        /// </summary>
        /// <param name="id"> Mandatory </param>
        /// <returns></returns>
        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _productRepository.GetProductById(id);
            return new OkObjectResult(product);
        }


        //POST api/values
        /// <summary>
        /// This method allows to create new product.
        /// </summary>
        /// <param name="product"> Mandatory </param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
            }
        }

        //PUT api/values/5
        /// <summary>
        /// This method allows to update information of a specific product wich is passed with ID
        /// </summary>
        /// 
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productRepository.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }


        //DELETE api/values/5
        /// <summary>
        /// This method allows to delete information about specific product which is passed with ID
        /// </summary>
        /// <param name="id"> Mandatory </param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return new OkResult();
        }
    }
}
