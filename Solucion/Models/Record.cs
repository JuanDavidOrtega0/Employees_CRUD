using System.ComponentModel.DataAnnotations.Schema;

namespace Solucion.Models
{
    public class Record
    {
        public int Id { get; set; }

        public System.DateTime? RegisterEntry { get; set; }

        public System.DateTime? RegisterExit { get; set; }

        [ForeignKey("Employee_Id")]
        public int Employee_Id { get; set; }
    }
}