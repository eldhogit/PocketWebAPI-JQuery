﻿using Newtonsoft.Json;
using Pocket.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Pocket.WebAPI.Controllers
{
    public class DefaultController : ApiController
    {
        PocketDb db;

        public DefaultController()
        {
            db = new PocketDb();
            //db.Configuration.ProxyCreationEnabled = false;
        }

      
        [Route("getallproducts")]
        [HttpGet]
        public IHttpActionResult GetAllProducts()
        {
          

            try
            {
                var products = (from PROD in db.Products
                                select PROD).ToList();

                //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                //var claimsUserId = principal.Claims.Where(c => c.Type == "sUserId").Single().Value;
                //var claimssUsername = principal.Claims.Where(c => c.Type == "sUsername").Single().Value;

                //TODO: Mapping
                List<ProductDTO> productDTOs = new List<ProductDTO>();
                foreach (var item in products)
                {
                    productDTOs.Add(new ProductDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        //Price = item.Price,
                        CreatedBy = item.CreatedBy,
                        ModifiedBy = item.ModifiedBy,
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate
                    });
                }


                return Ok(productDTOs);

            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;
                return InternalServerError();
            }

            
        }

        [Route("getproduct/{Id}")]
        [HttpGet]
        public IHttpActionResult GetProduct(long Id)
        {
           

            try
            {
                var product = (from PROD in db.Products
                                where PROD.Id == Id
                                select PROD).Single();
                return Ok(product);
            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;
                return InternalServerError();
            }


        }

       
        [Route("getproductPrice/{Id}")]
        [HttpGet]
        public IHttpActionResult GetProductPrice(long Id)
        {


            try
            {
                var ProductPrice = (from PROD in db.Products
                               where PROD.Id == Id
                               select PROD).Single().Price;
                return Ok(ProductPrice);
            }

            catch (Exception ex)
            {
                var errMsg = ex.Message;
                return InternalServerError();
            }

        }

        [Route("saveordermaster")]
        [HttpPost]
        public IHttpActionResult SaveOrderMaster(OrderMaster orderMaster)
        {
            try
            {
                orderMaster.CreatedDate = DateTime.Now;
                orderMaster.ModifiedDate = DateTime.Now;

                int count = orderMaster.OrderDetails.Count;
                var OrderDetails = orderMaster.OrderDetails.ToArray();

                for (int i = 0; i < count; i++)
                {
                    OrderDetails[i].CreatedDate = DateTime.Now;
                    OrderDetails[i].ModifiedDate = DateTime.Now;

                }

                db.OrderMasters.Add(orderMaster);
                db.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;

                return Content(HttpStatusCode.InternalServerError, false);
            }
        }

        [Route("getallorderdetails")]
        [HttpGet]
        public IHttpActionResult GetAllOrderDetails()
        {


            try
            {
                var OrderDetails = (from PROD in db.Products
                                    join ORDDT in db.OrderDetails
                                    on PROD.Id equals ORDDT.ProductId
                                    select new OrderDetV
                                    {
                                        Id = ORDDT.Id,
                                        OrderMasterId = ORDDT.OrderMasterId,
                                        ProductId = ORDDT.ProductId,
                                        Name = PROD.Name,
                                        Quantity = ORDDT.Quantity,
                                        Total = ORDDT.Total
                                    }).ToList();
                return Ok(OrderDetails);
            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;
                return InternalServerError();
            }


        }

       
        [Route("saveproduct")]
        [HttpPost]
        public IHttpActionResult SaveProduct([FromBody] Product product)
        {


            try
            {
                product.CreatedBy = 1;
                product.CreatedDate = DateTime.Now;
                product.ModifiedBy = 1;
                product.ModifiedDate = DateTime.Now;

                db.Products.Add(product);
                db.SaveChanges();

                //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                //var claimsUserId = principal.Claims.Where(c => c.Type == "sUserId").Single().Value;
                //var claimssUsername = principal.Claims.Where(c => c.Type == "sUsername").Single().Value;

                return Ok("New Id: " + product.Id);
            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;
                return BadRequest("Failed to insert!");

            }

        }

        [Route("saveproduct2")]
        [HttpPost]
        public IHttpActionResult SaveProduct2([FromBody] ProductDTO productDto)
        {
            try
            {
                Product product = new Product();
                if (ModelState.IsValid)
                {
                    
                    
                        product.Name = productDto.Name;
                        product.Price = productDto.Price;
                        product.CreatedBy = 1;
                        product.CreatedDate = DateTime.Now;
                        product.ModifiedBy = 1;
                        product.ModifiedDate = DateTime.Now;
                    

                    db.Products.Add(product);
                    db.SaveChanges();

                    return Ok("New Id: " + product.Id);
                }

                else
                {
                    var a = ModelState.Values;
                    var b = ModelState.Keys;
                    var c = ModelState.SelectMany(x => x.Value.Errors);
                    var d = JsonConvert.SerializeObject(ModelState.Values.Select(e => e.Errors).ToList());

                    var f = ModelState["Property"].Errors;





                    return BadRequest(ModelState);
                }


                //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                //var claimsUserId = principal.Claims.Where(c => c.Type == "sUserId").Single().Value;
                //var claimssUsername = principal.Claims.Where(c => c.Type == "sUsername").Single().Value;

               
            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;
                return BadRequest("Failed to insert!");
            }
        }

        [Route("saveproductfromuri")]
        [HttpPost]
        public IHttpActionResult SaveProductFromURI([FromUri] Product product)
        {


            try
            {
                product.CreatedBy = 1;
                product.CreatedDate = DateTime.Now;
                product.ModifiedBy = 1;
                product.ModifiedDate = DateTime.Now;

                db.Products.Add(product);
                db.SaveChanges();

                //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                //var claimsUserId = principal.Claims.Where(c => c.Type == "sUserId").Single().Value;
                //var claimssUsername = principal.Claims.Where(c => c.Type == "sUsername").Single().Value;

                return Ok("New Id: " + product.Id);
            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;
                return BadRequest("Failed to insert!");

            }

        }

        [Route("saveproductfromheader")]
        [HttpPost]
        public IHttpActionResult SaveProductFromHeader(Product product)
        {


            try
            {               

                product.CreatedBy = 1;
                product.CreatedDate = DateTime.Now;
                product.ModifiedBy = 1;
                product.ModifiedDate = DateTime.Now;

                db.Products.Add(product);
                db.SaveChanges();

                //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                //var claimsUserId = principal.Claims.Where(c => c.Type == "sUserId").Single().Value;
                //var claimssUsername = principal.Claims.Where(c => c.Type == "sUsername").Single().Value;

                return Ok("New Id: " + product.Id);
            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;
                return BadRequest("Failed to insert!");

            }

        }

    }
}
