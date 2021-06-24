﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PassionProjUditesh.Models;

namespace PassionProjUditesh.Controllers
{
    public class OrderDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/OrderData/ListOrders
        [HttpGet]
        [ResponseType(typeof(OrderDto))]
        public IEnumerable<OrderDto> ListOrders()
        {
            List<Order> Orders = db.Orders.ToList();
            List<OrderDto> OrderDtos = new List<OrderDto>();

            Orders.ForEach(o => OrderDtos.Add(new OrderDto()
            {
                OrderId = o.OrderId,
                OrderName = o.OrderName,
                ArrivedFrom = o.ArrivedFrom,
                DepartureTo = o.DepartureTo,
                OrderDateTime = o.OrderDateTime,
                CustomerName = o.Customer.CustomerName,
                CustomerMobNum = o.Customer.CustomerMobNum
            }));

            return OrderDtos;
        }

        // GET: api/OrderData/FindOrder/5
        [ResponseType(typeof(OrderDto))]
        [HttpGet]
        public IHttpActionResult FindOrder(int id)
        {
            Order order = db.Orders.Find(id);
            OrderDto OrdDto = new OrderDto()
            {
                OrderId = order.OrderId,
                OrderName = order.OrderName,
                ArrivedFrom = order.ArrivedFrom,
                DepartureTo = order.DepartureTo,
                OrderDateTime = order.OrderDateTime,
                CustomerName = order.Customer.CustomerName,
                CustomerMobNum = order.Customer.CustomerMobNum
            };
            if (order == null)
            {
                return NotFound();
            }

            return Ok(OrdDto);
        }

        // POST: api/OrderData/UpdateOrder/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.OrderId)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrderData/AddOrder
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult AddOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.OrderId }, order);
        }

        // POST: api/OrderData/DeleteOrder/5
        [ResponseType(typeof(Order))]
        [HttpPost]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.OrderId == id) > 0;
        }
    }
}