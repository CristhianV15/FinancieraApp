using FinancieraApp.Web.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinancieraApp.Web.Pages.DiagnosticoBD;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public bool ConexionExitosa { get; private set; }

    public int TotalBancos { get; private set; }

    public string MensajeError { get; private set; } = string.Empty;

    public async Task OnGetAsync()
    {
        try
        {
            TotalBancos = await _context.Bancos.CountAsync();
            ConexionExitosa = true;
        }
        catch (Exception ex)
        {
            ConexionExitosa = false;
            MensajeError = ex.Message;
        }
    }
}