namespace DunAndBradstreetProject.Models
{
    public class Order
    {
        public int Num { get; set; }

        public float Amount { get; set; }

        public float AdvanceAmount { get; set; }

        public DateTime Date { get; set; }

        public string CustCode { get; set; }

        public string AgentCode { get; set; }

        public string Description { get; set; }
    }
}
