﻿using System.ComponentModel.DataAnnotations;

namespace MyOwnSummary_WEB.Models.Dtos.UserDtos
{
    public class UserDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
