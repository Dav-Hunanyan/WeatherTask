namespace GlobalVariables
{
    public class WeatherDailyModel
    {
        public int Id { get; set; }

        public DateOnly Day { get; set; }

        public short? MaxTemp { get; set; }

        public short? MinTemp { get; set; }

        public ICollection<WeatherDayDetailModel>? WeatherDayDetailModels { get; set; }
    }
}
