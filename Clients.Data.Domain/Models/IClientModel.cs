using System;

namespace Clients.Domain.Models
{
    public interface IClientModel
    {
        int id { get; set; }

        string firstName { get; set; }

        string lastName { get; set; }

        string email { get; set; }

        string gender { get; set; }

        string ipAddress { get; set; }

        DateTime createdDate { get; set; }

        bool isDeleted { get; set; }
      
    }
}