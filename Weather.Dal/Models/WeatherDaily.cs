using System;
using System.Collections.Generic;

namespace Weather.Dal.Models;

public partial class WeatherDaily
{
    public int Id { get; set; }

    public DateOnly Day { get; set; }

    public short? MaxTemp { get; set; }

    public short? MinTemp { get; set; }

    public ICollection<WeatherDayDetail>? WeatherDayDetails { get; set; }
}
