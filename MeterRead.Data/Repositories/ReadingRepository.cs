using MeterRead.Data.Context;

namespace MeterRead.Data.Repositories
{
    public class ReadingRepository(MeterReadDbContext context) : Repository<Reading>(context)
    {
    }
}
