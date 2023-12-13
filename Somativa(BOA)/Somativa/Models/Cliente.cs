using System.ComponentModel;

namespace Somativa.Models
{
    public class Cliente
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        [DisplayName("Endereço")]
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        [DisplayName("CPF")]
        public string Cpf { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        public DateTime Nascimento { get; set; }
        public IEnumerable<Venda>? Venda { get; set; }
    }
}
