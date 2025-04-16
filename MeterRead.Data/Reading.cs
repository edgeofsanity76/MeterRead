using System;
using System.Collections.Generic;

namespace MeterRead.Data;

public partial class Reading
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string DateTime { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;
}
