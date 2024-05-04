using System;
using System.Collections.Generic;

namespace Weather.Dal.Models;

public partial class WeatherDayDetail
{
    public int DayId { get; set; }

    public TimeOnly Time { get; set; }

    public byte Temperature { get; set; }

    public byte? WeatherState { get; set; }

    public virtual WeatherDaily Day { get; set; } = null!;
}