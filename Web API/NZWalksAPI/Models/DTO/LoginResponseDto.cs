﻿namespace NZWalksAPI.Models.DTO
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string Token { get; set; }
    }
}
