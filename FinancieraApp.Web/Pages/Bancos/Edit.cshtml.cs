using FinancieraApp.Web.Data;
using FinancieraApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinancieraApp.Web.Pages.Bancos;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Banco Banco { get; set; } = new();

    public string MensajeError { get; private set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var banco = await _context.Bancos.FindAsync(id);

        if (banco is null)
        {
            return RedirectToPage("Index");
        }

        Banco = banco;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Banco.Nombre = Banco.Nombre?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(Banco.Nombre))
        {
            ModelState.AddModelError("Banco.Nombre", "El nombre del banco es obligatorio.");
        }

        var bancoActual = await _context.Bancos
            .AsNoTracking()
            .FirstOrDefaultAsync(banco => banco.Id == Banco.Id);

        if (bancoActual is null)
        {
            return RedirectToPage("Index");
        }

        var existeBancoGlobal = await _context.Bancos
            .AnyAsync(banco =>
                banco.Id != Banco.Id &&
                banco.UsuarioId == null &&
                banco.Nombre == Banco.Nombre);

        if (existeBancoGlobal)
        {
            ModelState.AddModelError("Banco.Nombre", "Ya existe un banco global registrado con ese nombre.");
        }

        if (!ModelState.IsValid)
        {
            Banco.EsSistema = bancoActual.EsSistema;
            Banco.UsuarioId = bancoActual.UsuarioId;
            return Page();
        }

        bancoActual.Nombre = Banco.Nombre;
        bancoActual.Activo = Banco.Activo;

        _context.Attach(bancoActual).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return RedirectToPage("Index");
    }
}