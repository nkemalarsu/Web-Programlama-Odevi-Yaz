using WebProgramlamaOdevi.Models.Common;

namespace WebProgramlamaOdevi.Models
{
    public class Animal:BaseEntity
    {
        public Guid AnimalAcceptedId { get; set; } = Guid.Empty;
        public Guid AnimalTypeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Age { get; set; }
        public bool isAdopted { get; set; } = false;
        public bool isConfirmed { get; set; } = false;
        public virtual AnimalType? AnimalType { get; set; }
        public Animal()
        {

        }
        public Animal(Guid animalTypeId, string? name, string? description, int age)
        {
            AnimalTypeId = animalTypeId;
            Name = name;
            Description = description;
            Age = age;
        }
    }
}
