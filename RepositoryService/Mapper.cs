using AutoMapper;

namespace RepositoreService
{
    public static class Mapper<T, S>
    {
        static MapperConfiguration configuration;
        static Mapper()
        {
            configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T, S>();
            });
        }
        public static S Map(T model)
        {
            IMapper iMapper = configuration.CreateMapper();
            S destination = iMapper.Map<T, S>(model);
            return destination;
        }
        public static List<S> MapCollection(List<T> model)
        {
            List<S> destination = new List<S>();
            foreach (var item in model)
            {
                destination.Add(Map(item));
            }
            return destination;
        }
    }
}
