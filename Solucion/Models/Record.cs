namespace Solucion.Models
{
    public class Record
    {
        public int Id { get; set; }

        public DateTime RegisterEntry { get; set; }

        public DateTime RegisterExit { get; set; }

        public int Employee_Id { get; set; }
    }
}