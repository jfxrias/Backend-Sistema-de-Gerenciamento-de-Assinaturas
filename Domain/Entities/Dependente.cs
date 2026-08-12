using System;

namespace Domain.Entities
{
    public class Dependente
    {
        public Guid Id { get; set; }
        public Guid AssinanteId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}