namespace FindHomeMadeFoodNearMe.Services.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var userlist = _repository.FindProvidersWithinRange(47.6395831, -122.1283810, 25);


            //List<ProviderInfo> providers = new List<ProviderInfo>();
            //foreach (var user in userlist)
            //{
            //    List<DishModel> menu = _repository.GetDishListByProviderId(user.UserId);

            //    StringBuilder menuString = new StringBuilder();

            //    foreach (var dish in menu)
            //    {
            //        menuString = menuString.Append(dish.DishName).Append("<br/>");
            //    }
            //    ProviderInfo provider = new ProviderInfo()
            //    {
            //        AddressString = user.GetAddressString(),
            //        ProvicerName = user.ProviderName,
            //        MenuString = menuString.ToString()
            //    };

            //    providers.Add(provider);

            //}

            //ViewBag.Providers = providers;
            return View();
        }
    }
}
