using System.ComponentModel.DataAnnotations;
using HotelWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace HotelWebApp.Models
{
    public class Hotel
    {
        public Hotel() { }
        public Hotel(string name, int stars, string address, string phoneNumbre)
        {
            this.Name = name;
            this.Stars = stars;
            this.Address = address;
            this.PhoneNumber = phoneNumbre;

        }

        public int Id { get; set; }

        [Required(ErrorMessage = ErrMsge.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = ErrMsge.RangoMinMax)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrMsge.Requerido)]
        [RegularExpression("([0-9]+)", ErrorMessage = ErrMsge.SoloNumeros)]
        public int Stars { get; set; }

        [Required(ErrorMessage = ErrMsge.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrMsge.RangoMinMax)]
        public string Address { get; set; }

        [StringLength(18, MinimumLength = 6, ErrorMessage = ErrMsge.RangoMinMax)]
        public string PhoneNumber { get; set; }


        public List<Room>? Rooms { get; set; }
    }
}
