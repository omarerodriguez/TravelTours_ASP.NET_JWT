﻿namespace TravelTours.Core.Entities
{
    public class User
    {
        public string UserName { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PassworSalt { get; set; }
    }
}
