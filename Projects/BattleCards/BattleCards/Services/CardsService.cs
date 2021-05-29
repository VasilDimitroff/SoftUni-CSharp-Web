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
        private readonly ApplicationDbContext dbContext;

        public CardsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddToCollection(int cardId, string userId)
        {
            var card = dbContext.Cards.Find(cardId);
            var user = dbContext.Users.Find(userId);

            if (dbContext.UsersCards.Any(x => x.UserId == user.Id && x.CardId == card.Id))
            {
                return;
            }

            var userCard = new UserCard()
            {
                Card = card,
                User = user,
                CardId = card.Id,
                UserId = user.Id
            };

            dbContext.UsersCards.Add(userCard);
            dbContext.SaveChanges();
        }

        public int Create(string name, string imagePath, string keyword, int attack, int health, string description)
        {
            Card card = new Card
            {
                Name = name,
                ImageUrl = imagePath,
                Keyword = keyword,
                Attack = attack,
                Health = health,
                Description = description
            };

            dbContext.Cards.Add(card);
            dbContext.SaveChanges();

            return card.Id;
        }

        public IEnumerable<CardViewModel> GetAllCards()
        {
            var cards = dbContext.Cards.Select(x => new CardViewModel
            {
                Attack = x.Attack,
                Description = x.Description,
                Id = x.Id,
                Health = x.Health,
                Image = x.ImageUrl,
                Keyword = x.Keyword,
                Name = x.Name
            })
                .ToList();

            return cards;
        }

        public IEnumerable<CardViewModel> GetCardsByUserId(string userId)
        {
            var cards = dbContext.UsersCards
                .Where(x => x.User.Id == userId)
                .Select(x => new CardViewModel
                    {
                        Attack = x.Card.Attack,
                        Description = x.Card.Description,
                        Id = x.Card.Id,
                        Health = x.Card.Health,
                        Image = x.Card.ImageUrl,
                        Keyword = x.Card.Keyword,
                        Name = x.Card.Name
                    })
               .ToList();

            return cards;
        }

        public void RemoveFromCollection(int cardId, string userId)
        {
            var cardToRemove = 
                dbContext.UsersCards
                .Where(c => c.Card.Id == cardId && c.User.Id == userId)
                .FirstOrDefault();

            if (cardToRemove != null)
            {
                dbContext.UsersCards.Remove(cardToRemove);
                dbContext.SaveChanges();
            }
        }
    }
}
