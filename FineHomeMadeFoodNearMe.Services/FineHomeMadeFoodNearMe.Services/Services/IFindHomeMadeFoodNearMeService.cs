﻿namespace FineHomeMadeFoodNearMe.Services.Services
{
    using FineHomeMadeFoodNearMe.Services.Models;
    using System.Collections.Generic;
    using FineHomeMadeFoodNearMe.Services.Models.Enums;

    public interface IFindHomeMadeFoodNearMeService
    {
        UserErrorModel RegisterUser(RegisterModel model);

        UserErrorModel LoginUser(LoginModel login);

        ErrorModel AddDishToMenu(AddDishModel model);

        List<DishModel> GetDishes(long providerId);

        ErrorModel RemoveDishFromMenu(RemoveDishModel removeModel);

        UserModel GetUser(long userId);

        ErrorModel PlaceOrder(PlaceOrderModel orderPlaceModel);

        List<UserModel> FindProvidersWithinRange(SearchFoodModel searchModel);

        List<UserModel> GetRegisteredUsers();

        ErrorModel UpdateOrderItemStatus(UpdateOrderItemModel updateModel);
    }
}
