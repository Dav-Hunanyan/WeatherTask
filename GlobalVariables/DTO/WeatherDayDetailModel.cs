namespace GlobalVariables
{
    public class WeatherDayDetailModel
    {
        public int DayId { get; set; }

        public TimeOnly Time { get; set; }

        public byte Temperature { get; set; }

        public WeatherState? WeatherState { get; set; }

        public virtual WeatherDailyModel DayModel { get; set; } = null!;
    }
}
