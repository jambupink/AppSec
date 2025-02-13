﻿namespace WebApplication13.Model
{
    public class UserSession
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
