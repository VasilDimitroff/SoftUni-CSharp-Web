using BattleCards.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        public int Create(string userId, string name, string imageUrl, string keyword, int attack, int health, string description);
        public void AddToCollection(string userId, int cardId);
        public void RemoveFromCollection(string userId, int cardId);
        public IEnumerable<CollectionCardViewModel> Collection(string userId);
        public IEnumerable<CollectionCardViewModel> All();
    }
}
