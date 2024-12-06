namespace api.Dtos
{
    public class ContainerAtPortDto
    {
        public string BicCode { get; set; }
        public decimal SizeTeu { get; set; }
        public bool IsEmpty { get; set; }
        public DateTime HandlingDate { get; set; }
    }
}
