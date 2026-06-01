using FinancieraApp.Web.Data;
using FinancieraApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinancieraApp.Web.Pages.Bancos;

public class CreateModel : PageModel
{
    private readonly AppDbContext _context;

    public CreateModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Banco Banco { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Banco.Nombre = Banco.Nombre?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(Banco.Nombre))
        {
            ModelState.AddModelError("Banco.Nombre", "El nombre del banco es obligatorio.");
        }

        var existeBancoGlobal = await _context.Bancos
            .AnyAsync(banco =>
                banco.UsuarioId == null &&
                banco.Nombre == Banco.Nombre);

        if (existeBancoGlobal)
        {
            ModelState.AddModelError("Banco.Nombre", "Ya existe un banco global registrado con ese nombre.");
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Banco.Activo = true;
        Banco.EsSistema = true;
        Banco.UsuarioId = null;

        _context.Bancos.Add(Banco);
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}