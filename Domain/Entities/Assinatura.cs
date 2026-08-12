using System;

namespace Domain.Entities
{
    public class Assinatura
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int MaxDependentes { get; set; }
        public int MaxTelas { get; set; }
        public decimal Valor { get; set; }
    }
}