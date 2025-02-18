using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Domain.Entities
{
    public class BaseEntity
    {
        [Column(Order = 0)]
        public Guid Id { get; set; }
        public string CreatedBy { get; set; } = "System";
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
