using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class LoteDto
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        [StringLength(100, MinimumLength=(5), ErrorMessage = "O {0} deve ter entre 5 e 100 caracteres.")]
        public string Nome { get; set; }
        
        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        [Range(0, 1000000, ErrorMessage = "O Preço deve ser entre 0 e 1.000.000.")]
        public decimal Preco { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        public string DataInicio { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        public string DataFim { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        [Range(2, 1000, ErrorMessage = "A Quantidade deve ser entre 2 e 1000.")]
        public int Quantidade { get; set; }
    }
}