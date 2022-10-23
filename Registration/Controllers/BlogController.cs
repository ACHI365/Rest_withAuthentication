using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registration.Data;
using Registration.Modules;
using Microsoft.IdentityModel.Tokens;

namespace Registration.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly DataContext dataContext;


        public BlogController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet, Authorize]
        [Route("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await dataContext.Blogs.ToListAsync());
        }

        [HttpGet("GetByAuthor{authorId}"), Authorize]
        public async Task<ActionResult<List<Blogs>>> Get(string authorId)
        {
            Blogs? dbblog = null;
            foreach (var blog in dataContext.Blogs)
            {
                if (blog.AuthorUser == authorId)
                {
                    dbblog = blog;
                    break;
                }
            }

            if (dbblog == null)
                return BadRequest("No such blog");
            return Ok(dbblog);
        }

        [HttpPost, Authorize]
        [Route("AddBlog")]
        public async Task<ActionResult<List<Blogs>>> AddBlog(Blogs blog)
        {
            Blogs finalBlog;
            finalBlog = blog;
            finalBlog.AuthorUser = RegistrationController.UserName;
            dataContext.Blogs.Add(finalBlog);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Blogs.ToListAsync());
        }

        [HttpPut, Authorize]
        [Route("UpdateBlog")]
        public async Task<ActionResult<List<Blogs>>> Update(Blogs request)
        {
            var dbHero = await dataContext.Blogs.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("No such blog");

            if (!request.AuthorUser.Equals(RegistrationController.UserName))
                return BadRequest("This user cannot change this blog");

            dbHero.Title = request.Title;
            dbHero.Body = request.Body;

            await dataContext.SaveChangesAsync();


            return Ok(await dataContext.Blogs.ToListAsync());
        }

        [HttpDelete("DeleteById{id}"), Authorize]
        public async Task<ActionResult<List<Blogs>>> Delete(int id)
        {
            var blog = await dataContext.Blogs.FindAsync(id);
            if (blog == null)
                return BadRequest("No such blog");

            if (!blog.AuthorUser.Equals(RegistrationController.UserName))
                return BadRequest("This user cannot change this blog");

            dataContext.Blogs.Remove(blog);
            await dataContext.SaveChangesAsync();
            return Ok(blog);
        }
    }
}