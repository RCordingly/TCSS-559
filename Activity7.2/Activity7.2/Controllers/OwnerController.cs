using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Activity7._2.Models;
using Newtonsoft.Json;

namespace Activity7._2.Controllers
{
    public class OwnerController : ApiController
    {
        static List<Owner> userDatabase = new List<Owner>()
        {
            new Owner{ username="robertcordingly@gmail.com", password="password", petIds = new string[] {"0", "1" } },
            new Owner{ username="g.cordingly@comcast.net", password="foobar", petIds = new string[] {"0"}}
        };

        //Get all of the pets.
        [HttpGet]
        [Route("api/owners")]
        public IEnumerable<Owner> getAllOwners()
        {
            return userDatabase;
        }

        [HttpPost]
        [Route("api/owners/validate")]
        public HttpResponseMessage validateUser()
        {
            try
            {
                string username = Request.Headers.GetValues("username").First();
                string password = Request.Headers.GetValues("password").First();
                if (isUser(username, password))
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                } else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            } catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/owners/getPets")]
        public HttpResponseMessage getRFIDs()
        {
            try
            {
                string username = Request.Headers.GetValues("username").First();
                Owner owner = findOwner(username);
                if (owner != null)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Headers.Add("Pets", owner.petIds);
                    return response;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("api/owners/addPet")]
        public HttpResponseMessage addPet()
        {
            try
            {
                string username = Request.Headers.GetValues("username").First();
                string pet = Request.Headers.GetValues("pet").First();
                Owner owner = findOwner(username);
                if (owner != null)
                {
                    string[] newArray = new string[owner.petIds.Length + 1];
                    for (int i = 0; i < owner.petIds.Length; i++)
                    {
                        newArray[i] = owner.petIds[i];
                    }
                    newArray[owner.petIds.Length] = pet;
                    owner.petIds = newArray;
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private string doBasicRequest(string url, string method, string[] headers)
        {
            HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
            serviceRequest.Method = method;

            for (var i = 0; i < headers.Length; i += 2)
            {
                serviceRequest.Headers.Add(headers[i], headers[i + 1]);
            }

            try
            {
                HttpWebResponse serviceResponse = (HttpWebResponse)serviceRequest.GetResponse();
                Stream receiveStream = serviceResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(receiveStream, encode, true);
                return readStream.ReadToEnd();

            }
            catch (System.Net.WebException e)
            {
                return "ERROR";
            }
        }

        //Create a new owner or update an existing ones password.
        [HttpPost]
        [Route("api/owners")]
        public HttpResponseMessage createOwner([FromBody] Owner owner)
        {
            if (owner == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, owner);
            }


            string username = owner.username;
            owner.password = Request.Headers.GetValues("password").First();
            string newPassword = Request.Headers.GetValues("newPassword").First();

            if (owner.password.Equals(newPassword) || newPassword == null) { 
                
                //Make sure a owner does not already exist.
                if (findOwner(owner.username) != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }

                string url = "https://metropolis-api-email.p.rapidapi.com/analysis?email=" + username;
                string[] headers = { "X-RapidAPI-Key", "gZi9wXlPr0mshb5QAyNa6Fih9ywpp1yRBpdjsnkGyjBpRededz" };
                string result = doBasicRequest(url, "GET", headers);

                if (!result.Equals("ERROR"))
                {
                    dynamic emailCheck = JsonConvert.DeserializeObject(result);
                    if ((bool)emailCheck.valid)
                    {
                        userDatabase.Add(owner);
                        return Request.CreateResponse(HttpStatusCode.Created, owner.username);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                    }
                            
                } else
                {

                    return Request.CreateResponse(HttpStatusCode.BadGateway);
                }

            } else
            {
                if (newPassword != null)
                {
                    findOwner(username).password = newPassword;
                    return Request.CreateResponse(HttpStatusCode.OK, owner.username);
                } else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }

        }

        private Boolean isUser(string username, string password)
        {
            return (findOwner(username).password.Equals(password));
        }

        private Owner findOwner(string username)
        {
            Owner result = (from owner in userDatabase
                        where owner.username.Equals(username)
                        select owner).FirstOrDefault();
            return result;
        }
    }
}
