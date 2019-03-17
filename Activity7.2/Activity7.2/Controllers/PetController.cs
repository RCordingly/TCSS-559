using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Activity7._2.Models;
using System.Xml;
using System;

namespace Activity7._2.Controllers
{
    public class PetController : ApiController
    {
        //The database of pet objects.
        static List<Pet> petDatabase = new List<Pet>()
        {
            new Pet{ id = 0, petName = "Daisy", petType = "Dog", petBreed = "yellow lab", petVaccinations = "ALL", petAge = "8", ownerName = "Robert Cordingly", ownerEmail="robertcordingly@gmail.com", ownerPhone="2533813091"},
            new Pet{ id = 1, petName = "Cooper", petType = "Dog", petBreed = "black lab", petVaccinations = "None", petAge = "1", ownerName = "Gary", ownerZip="98498"}
        };

        //Get all of the pets.
        [HttpGet]
        [Route("api/pet")]
        public IEnumerable<Pet> getAllPets()
        {
            return petDatabase;
        }

        //Get pet by #id.
        [HttpGet]
        [Route("api/pet/id/{id:int}")]
        public HttpResponseMessage getPet(int id)
        {
            Pet result = getPetById(id);

            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            if (result == null)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
        }

        //Create a new pet and add it to the list.
        [HttpPost]
        [Route("api/pet")]
        public HttpResponseMessage createPet([FromBody] Pet pet)
        {
            int newId = pet.id;

            //Make sure a pet does not already exist.
            if (getPetById(newId) != null)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            petDatabase.Add(pet);
                
            return Request.CreateResponse(HttpStatusCode.Created, pet);
          
        }

        //Remove a pet from the database.
        [HttpDelete]
        [Route("api/pet/id/{id:int}")]
        public HttpResponseMessage deletePet(int id)
        {
            Pet result = getPetById(id);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            petDatabase.Remove(result);

            return response;
        }

        //Replace pet #id with a new pet.
        [HttpPut]
        [Route("api/pet/id/{id:int}")]
        public HttpResponseMessage putPet(int id, [FromBody] Pet pet)
        {
            Pet oldPet = getPetById(id);

            //The pet being replaced must have the same id as the new pet.
            if (oldPet == null || oldPet.id != pet.id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            petDatabase.Add(pet);
            petDatabase.Remove(oldPet);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        [HttpPost]
        [Route("api/pet/vax")]
        public XmlDocument getVaxxed()
        {
            XmlDocument dom = new XmlDocument();
            XmlElement locationStats = dom.CreateElement("VaxStats");
            dom.AppendChild(locationStats);

            int age = 0;
            try
            {
                age = Int32.Parse(Request.Headers.GetValues("age").First());
                string type = Request.Headers.GetValues("type").First();

                if (!type.Equals("Dog") && !type.Equals("dog"))
                {
                    XmlElement field1 = dom.CreateElement("message");
                    locationStats.AppendChild(field1);
                    XmlText text1 = dom.CreateTextNode("Vaccination recommendations only available for dogs.");
                    field1.AppendChild(text1);
                    return dom;
                }

                string nextVaccination = "0";

                switch (age)
                {
                    case (0):
                        nextVaccination = "Distemper, measles, parainfluenva, bordatella. At 6 to 8 weeks old.\nDHPP every 4 weeks (3 doses).";
                        break;
                    case (1):
                        nextVaccination = "DHPP and rabies at 1 year old.";
                        break;
                    case (2):
                        nextVaccination = "DHPP and rabies at 3 years old.";
                        break;
                    case (3):
                        nextVaccination = "DHPP and rabies at 5 years old.";
                        break;
                    case (4):
                        nextVaccination = "DHPP and rabies at 5 years old.";
                        break;
                }
                if (age >= 5)
                {
                    nextVaccination = "DHPP every 1 to 2 years. Rabies every 1 to 3 years.";
                }

                XmlElement field = dom.CreateElement("message");
                locationStats.AppendChild(field);
                XmlText text = dom.CreateTextNode(nextVaccination);
                field.AppendChild(text);
                return dom;
            }
            catch (Exception e)
            {
                XmlElement field = dom.CreateElement("message");
                locationStats.AppendChild(field);
                XmlText text = dom.CreateTextNode("Failed to load vaccinations recommendations.");
                field.AppendChild(text);
                return dom;
            }
        }

        /**
         * Helper method used to query the list of pets with LINQ
         */
        private Pet getPetById(int id)
        {
            Pet aPet = (from pet in petDatabase
                       where pet.id.Equals(id)
                       select pet ).FirstOrDefault();
            return aPet;
        }
    }
}
