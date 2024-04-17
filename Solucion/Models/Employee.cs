namespace Solucion.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }

        public required string Address { get; set; }

        public required string Password { get; set; }
    }
}