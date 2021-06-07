using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards.ViewModels
{
    public class CollectionCardViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
    }
}
