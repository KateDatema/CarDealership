using System;
using System.Collections.Generic;

#nullable disable

namespace CarDealershipProject.Models
{
    public partial class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string Color { get; set; }
        public string Photo { get; set; }
    }
}
