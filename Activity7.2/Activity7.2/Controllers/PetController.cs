using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Activity7._2.Models;

namespace Activity7._2.Controllers
{
    public class PetController : ApiController
    {
        //The database of pet objects.
        static List<Pet> petDatabase = new List<Pet>()
        {
            new Pet{ id = 0, petName = "Daisy", petType = "Dog", petBreed = "Labrador", petVaccinations = "ALL", petAge = "8", ownerName = "Robert Cordingly", ownerEmail="robertcordingly@gmail.com", ownerPhone="2533813091"},
            new Pet{ id = 1, petName = "Cooper", petType = "Dog", petBreed = "Labrador", petVaccinations = "None", petAge = "1", ownerName = "Gary", ownerZip="98498"}
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
            if (getPetById(newId) == null)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict);
            }

            petDatabase.Add(pet);

            var response = Request.CreateResponse(HttpStatusCode.Created, pet);
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
