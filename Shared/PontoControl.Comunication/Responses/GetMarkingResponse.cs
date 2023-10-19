using PontoControl.Domain.Entities;

namespace PontoControl.Comunication.Responses
{
    public class GetMarkingResponse
    {
        public List<Marking> Marking { get; set; }
        public DateTime Date { get; set; }
        public decimal? TotalHoursByDay { get; set; }
    }
}
