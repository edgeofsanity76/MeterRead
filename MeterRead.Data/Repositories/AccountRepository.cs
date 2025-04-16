using MeterRead.Data.Context;

namespace MeterRead.Data.Repositories
{
    public class AccountRepository(MeterReadDbContext context) : Repository<Account>(context);
}
