namespace StockManagement.Repositories;
using StockManagement.Models;
using StockManagement.Data;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }

   
}