using MeterRead.Data.Context;
using MeterRead.Data.Entities;

namespace MeterRead.Data.Repositories;

public class ReadingRepository(MeterReadDbContext context) : Repository<Reading>(context);