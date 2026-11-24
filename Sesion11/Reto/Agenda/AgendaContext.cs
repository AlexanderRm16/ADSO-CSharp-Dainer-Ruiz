using Microsoft.EntityFrameworkCore;

namespace Agenda;

public class AgendaContext : DbContext
{
    public DbSet<Contacto> Contactos => Set<Contacto>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=agenda.db");
}
