using BattleCards.Services;
using BattleCards.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var cards = cardsService.GetAllCards();
            return this.View(cards);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CardInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.Name) || input.Name.Length < 5 || input.Name.Length > 15)
            {
                return this.Error("Name must be between 5 and 15 characters long!");
            }

            if (string.IsNullOrWhiteSpace(input.Image))
            {
                return this.Error("Please enter a valid URL!");
            }

            if (string.IsNullOrWhiteSpace(input.Keyword))
            {
                return this.Error("Please enter a valid keyword!");
            }

            if (input.Attack < 0)
            {
                return this.Error("Attack cannot be a negative number!");
            }

            if (input.Health < 0)
            {
                return this.Error("Health cannot be a negative number!");
            }

            if (string.IsNullOrEmpty(input.Description) || input.Description.Length > 200)
            {
                return this.Error("Description cannot be empty or above 200 characters long!");
            }

            var cardId = cardsService.Create(input.Name, input.Image, input.Keyword, input.Attack, input.Health, input.Description);
            cardsService.AddToCollection(cardId, this.GetUserId());
            
            return this.Redirect("/Cards/All");
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            List<CardViewModel> cards = cardsService.GetCardsByUserId(this.GetUserId()).ToList();
            return this.View(cards);
        }

      
        public HttpResponse AddToCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            cardsService.AddToCollection(cardId, this.GetUserId());
            return this.Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(int cardId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            cardsService.RemoveFromCollection(cardId, this.GetUserId());
            return this.Redirect("/Cards/Collection");
        }
    }
}
