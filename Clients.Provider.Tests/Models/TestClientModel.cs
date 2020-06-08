using System;
using Clients.Domain.Models;

namespace Client.Services.Tests.Models
{
    internal class TestClientModel : IClientModel
    {
        public int id { get; set; }

        public string firstName { get; set; } = "firstName";

        public string lastName { get; set; } = "lastName";

        public string email { get; set; } = "email";

        public string gender { get; set; } = "male";

        public string ipAddress { get; set; } = "37.84.185.82";

        public bool isDeleted { get; set; }

    }
}