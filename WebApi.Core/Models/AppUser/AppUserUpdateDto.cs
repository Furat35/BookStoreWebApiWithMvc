﻿namespace WebApi.Core.Models.AppUser
{
    public class AppUserUpdateDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
