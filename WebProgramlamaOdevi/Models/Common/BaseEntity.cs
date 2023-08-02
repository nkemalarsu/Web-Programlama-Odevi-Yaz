namespace WebProgramlamaOdevi.Models.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public BaseEntity()
        {
            
        }
        public BaseEntity(Guid Id)
        {
            this.Id = Id;
        }
    }
}
