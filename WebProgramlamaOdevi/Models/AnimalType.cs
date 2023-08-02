using WebProgramlamaOdevi.Models.Common;

namespace WebProgramlamaOdevi.Models
{
    public class AnimalType:BaseEntity  
    {
        public string? Name { get; set; }
        public virtual ICollection<Animal> Animals { get; set; }
        public AnimalType()
        {

        }
        public AnimalType(string? name)
        {
            Name = name;
        }
    }
}
