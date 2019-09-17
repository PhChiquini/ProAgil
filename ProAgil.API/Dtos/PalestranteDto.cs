using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class PalestranteDto
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        public string Nome { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        [MinLength(50, ErrorMessage = "O {0} deve conter no mínimo 50 caracteres.")]
        public string MiniCurriculo { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        public string ImagemURL { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        public string Telefone { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        [EmailAddress (ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }        
        public List<EventoDto> Eventos { get; set; }
    }
}