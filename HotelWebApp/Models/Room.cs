using HotelWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelWebApp.Models
{
    public class Room
    {
        public Room() { }
        public Room(int id) {
          HotelId = id;
        
        }

        public int Id { get; set; }

        [Required(ErrorMessage = ErrMsge.Requerido)]
        [RegularExpression("([0-9]+)", ErrorMessage = ErrMsge.SoloNumeros)]
        public int Number { get; set; }

        [Required(ErrorMessage = ErrMsge.Requerido)]
        [RegularExpression("([0-9]+)", ErrorMessage = ErrMsge.SoloNumeros)]
        public int MaxGuests { get; set; }

        public int HotelId { get; set; }

        public Hotel? Hotel { get; set; }
    }
}