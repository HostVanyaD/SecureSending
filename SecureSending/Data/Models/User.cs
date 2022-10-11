﻿namespace SecureSending.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [StringLength(20)]
        public string Username { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public string Link { get; set; }
    }
}