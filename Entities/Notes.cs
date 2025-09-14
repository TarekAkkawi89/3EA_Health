namespace _3EA_Health.Entities
{
    public class Notes
    {
        public int Id { get; set; }

        public int tenantId { get; set; }

        public int patientId { get; set; }

        public string author { get; set; }

        public string text { get; set; }

        public DateTime createDate { get; set; }
    }
}
