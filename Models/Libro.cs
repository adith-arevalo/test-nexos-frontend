
namespace FApiAutors.Models
{
    public class Libro
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string Genero { get; set; }
        public int NumeroPaginas { get; set; }
        public List<AutorLibro> AutoresLibros { get; set; }
    }
}
