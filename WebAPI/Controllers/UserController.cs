using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using System.Data.Entity;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {

        public IHttpActionResult GetAllUsers()
        {
            IList<UserViewModel> users = null;

            using (var ctx = new TrialCoreDBEntities())
            {
                users = ctx.Users.Include("Users")
                            .Select(s => new UserViewModel()
                            {
                                UserId = s.UserId,
                                UserName = s.UserName
                                
                                
                            }).ToList<UserViewModel>();
            }

            if (users.Count == 0)
            {
                return NotFound();
            }
           
            return Ok(users);
        }

        public IHttpActionResult GetAllUsers(int userid)
        {
            IList<UserViewModel> users = null;

            using (var ctx = new TrialCoreDBEntities())
            {
                users = ctx.Users.Include("Users")
                    .Where(s => s.UserId == userid)
                    .Select(s => new UserViewModel()
                    {
                        UserId = s.UserId,
                        UserName = s.UserName
                        
                    }).ToList<UserViewModel>();
            }

            if (users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }


        public IHttpActionResult PostNewStudent(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TrialCoreDBEntities())
            {
                ctx.Users.Add(new User()
                {
                    UserId = user.UserId,
                    UserName = user.UserName
                });

                ctx.SaveChanges();
            }

            return Ok();
        }


        public IHttpActionResult Put(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TrialCoreDBEntities())
            {
                var existingUser = ctx.Users.Where(s => s.UserId == user.UserId)
                                                        .FirstOrDefault<User>();

                if (existingUser != null)
                {
                    existingUser.UserName = user.UserName;
                    

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid user id");

            using (var ctx = new TrialCoreDBEntities())
            {
                var user = ctx.Users
                    .Where(s => s.UserId == id)
                    .FirstOrDefault();

                ctx.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

    }
}
