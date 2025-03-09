using Repository;
using Services.Interfaces;
namespace Services.Application
{
    public class HomeService: IHomeService
    {
        private readonly HomeRepository homeData;
        public HomeService(HomeRepository _homeData)
        {
            homeData = _homeData;
        }

        public string Test()
        {
            return homeData.Test();
        }
    }
}
