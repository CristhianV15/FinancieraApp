using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancieraApp.Web.Models;

[Table("Banco")]
public class Banco
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del banco es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre del banco no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    public bool Activo { get; set; } = true;

    public bool EsSistema { get; set; }

    public Guid? UsuarioId { get; set; }
}