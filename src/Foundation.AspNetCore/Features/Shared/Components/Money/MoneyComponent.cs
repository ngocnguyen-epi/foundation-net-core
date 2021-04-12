﻿using Mediachase.Commerce;
using Microsoft.AspNetCore.Mvc;

namespace Foundation.AspNetCore.Features.Shared.Components.Money
{
    public class DropdownComponent : ViewComponent
    {
        public IViewComponentResult Invoke(decimal amount, Currency currency)
        {
            var money = new Mediachase.Commerce.Money(amount, currency);
            return View("~/Features/Shared/Components/Money/Default.cshtml", money);
        }
    }
}
