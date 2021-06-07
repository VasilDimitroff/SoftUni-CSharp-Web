using BattleCards.Services;
using BattleCards.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardsService cardsService;

        public CardsController(ICardsService cardsService)
        {
            this.cardsService = cardsService;
        }

        public HttpResponse Add()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CardInputModel input)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(input.Name) || input.Name.Length < 5 || input.Name.Length > 15)
            {
                return this.Error("Name should be betwen 5 and 15 characters long");
            }

            if (string.IsNullOrWhiteSpace(input.Description) || input.Description.Length > 200)
            {
                return this.Error("Description cannot be above 200 characters long");
            }

            if (string.IsNullOrWhiteSpace(input.Image))
            {
                return this.Error("Please enter a valid image URL");
            }

            if (string.IsNullOrWhiteSpace(input.Keyword))
            {
                return this.Error("Keyword cannot be empty");
            }

            if (input.Attack < 0)
            {
                return this.Error("Attack cannot be negative number");
            }

            if (input.Health < 0)
            {
                return this.Error("Health cannot be negative number");
            }

            cardsService.Create(GetUserId(), input.Name, input.Image, input.Keyword, input.Attack, input.Health, input.Description);

            return Redirect("/Cards/All");
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var cards = cardsService.All();
            return this.View(cards);
        }

        public HttpResponse AddToCollection(int cardId)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            cardsService.AddToCollection(GetUserId(), cardId);

            return Redirect("/Cards/All");
        }


        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            cardsService.RemoveFromCollection(GetUserId(), cardId);

            return Redirect("/Cards/Collection");
        }

        public HttpResponse Collection()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/Users/Login");
            }

            var collection = cardsService.Collection(GetUserId());

            return this.View(collection);
        }
    }
}
