using BattleCards.Data;
using BattleCards.Models;
using BattleCards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleCards.Services
{
    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddToCollection(string userId, int cardId)
        {
            if (db.UsersCards.Any(x => x.UserId == userId && x.CardId == cardId))
            {
                return;
            }

            var userCard = new UserCard
            {
                CardId = cardId,
                UserId = userId
            };

            db.UsersCards.Add(userCard);
            db.SaveChanges();
        }

        public IEnumerable<CollectionCardViewModel> All()
        {
            var allCards = db.Cards
                .Select(x => new CollectionCardViewModel
                {
                    Id = x.Id,
                    Attack = x.Attack,
                    Description = x.Description,
                    Health = x.Health,
                    ImagePath = x.ImageUrl,
                    Keyword = x.Keyword,
                    Name = x.Name
                })
                .ToList();

            return allCards;
        }

        public IEnumerable<CollectionCardViewModel> Collection(string userId)
        {
            var allCards = db.Cards
                .Where(x => x.CardUsers.Any(u => u.UserId == userId))
               .Select(x => new CollectionCardViewModel
               {
                   Id = x.Id,
                   Attack = x.Attack,
                   Description = x.Description,
                   Health = x.Health,
                   ImagePath = x.ImageUrl,
                   Keyword = x.Keyword,
                   Name = x.Name
               })
               .ToList();

            return allCards;
        }

        public int Create(string userId, string name, string imageUrl, string keyword, int attack, int health, string description)
        {
            var card = new Card()
            {
                Attack = attack,
                Description = description,
                Health = health,
                ImageUrl = imageUrl,
                Keyword = keyword,
                Name = name,
            };

            db.Cards.Add(card);
            db.SaveChanges();

            var user = db.Users.Find(userId);
            //var createdCard = db.Cards.OrderByDescending(x => x.Id).FirstOrDefault();

            AddToCollection(user.Id, card.Id);

            return card.Id;
        }

        public void RemoveFromCollection(string userId, int cardId)
        {
            var cardToRemove = db.UsersCards.FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);

            db.UsersCards.Remove(cardToRemove);
            db.SaveChanges();
        }
    }
}
