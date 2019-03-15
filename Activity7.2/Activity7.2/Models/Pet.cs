using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activity7._2.Models
{
    //Pet object, used to store all data associated with a pet.
    public class Pet
    {
        public int id { get; set; }
        public string petName { get; set; }
        public string petType { get; set; }
        public string petBreed { get; set; }
        public string petVaccinations { get; set; }
        public int petAge { get; set; }

        public string ownerName { get; set; }
        public string ownerEmail { get; set; }
        public string ownerAddress { get; set; }
        public int ownerZip { get; set; }
    }
}