using System;
using System.Collections.Generic;

namespace MeterRead.Data;

public partial class Account
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Reading> Readings { get; set; } = new List<Reading>();
}
