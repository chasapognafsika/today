using Clients.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clients.WebApi.Models
{
    public class ClientDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string IpAddress { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}


