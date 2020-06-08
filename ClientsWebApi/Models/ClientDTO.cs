using Clients.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.WebApi.Models
{
    public class ClientDTO : IClientModel
    {
        public int id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public string ipAddress { get; set; }

        public bool isDeleted { get; set; }
    }
}


