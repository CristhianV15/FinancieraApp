using FinancieraApp.Web.Data;
using FinancieraApp.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinancieraApp.Web.Pages.Bancos;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Banco> Bancos { get; private set; } = new List<Banco>();

    public async Task OnGetAsync()
    {
        Bancos = await _context.Bancos
            .OrderBy(banco => banco.Nombre)
            .ToListAsync();
    }
}