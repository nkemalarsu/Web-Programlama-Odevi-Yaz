namespace WebProgramlamaOdevi.Models.Common
{
    public class BaseEntity
    {
        public string Id { get; set; }= Guid.NewGuid().ToString();
        public BaseEntity()
        {
            
        }
        public BaseEntity(Guid Id)
        {
            this.Id = Id.ToString();
        }
    }
}
