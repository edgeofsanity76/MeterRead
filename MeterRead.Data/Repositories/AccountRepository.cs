using MeterRead.Data.Context;
using MeterRead.Data.Entities;

namespace MeterRead.Data.Repositories;

public class AccountRepository(MeterReadDbContext context) : Repository<Account>(context);