using BattleCards.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        public IEnumerable<CardViewModel> GetCardsByUserId(string userId);
        public int Create(string name, string imagePath, string keyword, int attack, int health, string description);
        public IEnumerable<CardViewModel> GetAllCards();
        public void AddToCollection(int cardId, string userId);
        public void RemoveFromCollection(int cardId, string userId);
    }
}
