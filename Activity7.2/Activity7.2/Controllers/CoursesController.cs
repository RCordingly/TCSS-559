using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Activity7._2.Models;

//Robert Cordingly
//Web Services In-Class Activity
//This controller manages a simple course catalogue.
namespace Activity7._2.Controllers
{
    public class CoursesController : ApiController
    {
        static List<Course> UWCourses = new List<Course>()
        {
            new Course{ id=1, courseCode="TCSS 445", courseName="Databases", deptName="CS"},
            new Course{ id=2, courseCode="TCSS 559", courseName="Web Services", deptName="CS"},
            new Course{ id=3, courseCode="TCSS 555", courseName="Machine Learning", deptName="CS"}
        };

        //Get all of the courses.
        [HttpGet]
        [Route("")]
        public IEnumerable<Course> getAllCourses()
        {
            return UWCourses;
        }

        //Get course #id.
        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage getCourse(int id)
        {
            if (id < 0 || id >= UWCourses.Count())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, UWCourses[id]);
            response.Headers.Add("id", UWCourses[id].id.ToString());
            response.Headers.Add("courseCode", UWCourses[id].courseCode);
            response.Headers.Add("courseName", UWCourses[id].courseName);
            response.Headers.Add("department", UWCourses[id].deptName);
            return response;
        }

        //Get a course from the lists with id.
        [HttpGet]
        [Route("{id:int}/name")]
        public HttpResponseMessage getCourseName(int id)
        {
            //LINQ:
            var aCourse = (from a in UWCourses
                           where a.id.Equals(id)
                           select new { a.courseName }).FirstOrDefault();

            var response = Request.CreateResponse(HttpStatusCode.OK, aCourse);
            return response;
        }

        //Create a new course and add it to the list.
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateCourse([FromBody] Course course)
        {
            UWCourses.Add(course);

            var response = Request.CreateResponse(HttpStatusCode.OK, UWCourses);
            return response;
        }

        //Replace course #id with a new course.
        [HttpPut]
        [Route("{id:int}")]
        public HttpResponseMessage putCourse(int id, [FromBody] Course course)
        {
            if (id < 0 || id >= UWCourses.Count())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            UWCourses[id] = course;

            var response = Request.CreateResponse(HttpStatusCode.OK, UWCourses);
            return response;
        }

        //Delete a course from the list.
        [HttpDelete]
        [Route("{id:int}")]
        public HttpResponseMessage deleteCourse(int id)
        {
            if (id < 0 || id >= UWCourses.Count())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            UWCourses.RemoveAt(id);

            var response = Request.CreateResponse(HttpStatusCode.OK, UWCourses);
            return response;
        }
    }
}