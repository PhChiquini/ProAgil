using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        [Range(5,100, ErrorMessage = "O {0} deve ter entre 5 e 100 caracteres.")]
        public string Local { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        public string DataEvento { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        [Range(5,100, ErrorMessage = "O {0} deve ter entre 5 e 100 caracteres.")]
        public string Tema { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        [Range(10, 120000, ErrorMessage = "Quantidade de Pessoas deve ser entre 10 e 120.000.")]
        public int QtdPessoas { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        public string ImagemURL { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        public string Telefone { get; set; }

        [Required (ErrorMessage = "Campo {0} é obrigatório.")]
        [EmailAddress (ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<PalestranteDto> Palestrantes { get; set; }
    }
}