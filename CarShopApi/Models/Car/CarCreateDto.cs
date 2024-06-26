﻿using System.ComponentModel.DataAnnotations;

namespace CarShopApi.Models.Car
{
    public class CarCreateDto
    {
        [Required]
        public int ProducerId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Model { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public string? OryginalImageName { get; set; }
    }
}
