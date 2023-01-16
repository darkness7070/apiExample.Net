using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication8.Entities;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class usersController : ApiController
    {
        private mobappEntities db = new mobappEntities();

        // GET: api/users
        [ResponseType(typeof(List<users>))]
        public IHttpActionResult Getusers()
        {
            return Ok(db.users.ToList().ConvertAll(x => new respUsers(x)).ToList());
        }

        // GET: api/users/5
        [ResponseType(typeof(users))]
        public IHttpActionResult Getusers(int id)
        {
            users users = db.users.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // PUT: api/users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putusers(int id, users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.id)
            {
                return BadRequest();
            }

            db.Entry(users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usersExists(id))
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

        // POST: api/users
        [ResponseType(typeof(users))]
        public IHttpActionResult Postusers(users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(users);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (usersExists(users.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = users.id }, users);
        }

        // POST: api/login
        [ResponseType(typeof(users))]
        public IHttpActionResult postLogin(string login, string password)
        {
            var user = db.users.Where(x => x.login == login).FirstOrDefault();
            if (user == null) return Ok("user not found");
            if (user.password != password) return Ok("password invalid");
            return Ok(user);
        }

        // DELETE: api/users/5
        [ResponseType(typeof(users))]
        public IHttpActionResult Deleteusers(int id)
        {
            users users = db.users.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            db.users.Remove(users);
            db.SaveChanges();

            return Ok(users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usersExists(int id)
        {
            return db.users.Count(e => e.id == id) > 0;
        }
    }
}