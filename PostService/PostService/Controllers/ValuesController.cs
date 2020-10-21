using Newtonsoft.Json;
using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace PostService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        PostBookEntities p;
        public HttpResponseMessage Get()
        {
            //return new string[] { "value1", "value2" };
            List<Post> PostList = new List<Post>();
            List<FeedBack> fb = new List<FeedBack>();
            List<Comment> CommentList = new List<Comment>();
            //List<EmployeeDetails> dt = new List<EmployeeDetails>();
            //dt = e.EmployeeDetails.ToList();
            p = new PostBookEntities();
            PostList = p.Posts.ToList();
            CommentList = p.Comments.ToList();

            fb = (from Post p in PostList
                  select new FeedBack
                  {
                      PostID = p.PostID,
                      Title = p.Title,
                      Details = p.Details,
                      AddedBy = p.AddedBy,
                      User = p.User,
                      DateAdded = p.DateAdded,
                      Comments = (from Comment c in CommentList
                                  where p.PostID == c.PostID
                                  select new Comment
                                  {
                                      CommentID = c.CommentID,
                                      Comment1 = c.Comment1.ToString(),
                                      DateAdded = c.DateAdded,
                                      AddedBy = c.AddedBy
                                  }).ToList(),
                  }).ToList();


            HttpResponseMessage response = null;
            string json = string.Empty;

            json = JsonConvert.SerializeObject(fb, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            // Info.  
            return response;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
