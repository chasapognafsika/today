using System;
using Clients.Domain.Models;

namespace Client.Services.Tests.Models
{
    internal class TestClientModel : IClientModel
    {
        public int id { get; set; }

        public string firstName { get; set; } = Guid.NewGuid().ToString();

        public string lastName { get; set; } = Guid.NewGuid().ToString();

        public string email { get; set; } = Guid.NewGuid().ToString();

        public string gender { get; set; } = Guid.NewGuid().ToString();

        public string ipAddress { get; set; } = Guid.NewGuid().ToString();

        public DateTime createdDate { get; set; }

        public bool isDeleted { get; set; }
    
    }
}