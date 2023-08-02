using Microsoft.AspNetCore.Identity;
using WebProgramlamaOdevi.Models.Common;

namespace WebProgramlamaOdevi.Models
{
    public class AnimalAdopted:BaseEntity
    {
        public string? UserId { get; set; }
        public string AnimalId { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public bool isConfirmed { get; set; } = false;
        public DateTime ConfirmedDateTime { get; set; }

        public AnimalAdopted()
        {
            
        }
        public AnimalAdopted(string? userId, string animalId, DateTime createdDateTime, bool isConfirmed, DateTime confirmedDateTime)
        {
            UserId = userId;
            AnimalId = animalId;
            CreatedDateTime = createdDateTime;
            this.isConfirmed = isConfirmed;
            ConfirmedDateTime = confirmedDateTime;
        }

        public virtual Animal? Animal { get; set; }
        public virtual IdentityUser? IdentityUser { get; set; }
    }
}
