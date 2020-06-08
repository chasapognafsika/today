using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clients.Data.Entities
{
    [Table("Client")]
    public class ClientEntity
    {
        [Key]
        public int id { get; set; }

        [Required, MaxLength(50)]
        public string firstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string lastName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string email { get; set; } = string.Empty;

        [Required, MaxLength(10)]
        public string gender { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string ipAddress { get; set; } = string.Empty;

        public bool deleted { get; set; }
    }

}