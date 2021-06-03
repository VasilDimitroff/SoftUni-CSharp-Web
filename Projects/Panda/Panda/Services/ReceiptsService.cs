using Panda.Data;
using Panda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Panda.Services
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly ApplicationDbContext db;

        public ReceiptsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Create(string recipientId, string packageId)
        {
            var recipient = db.Users.Find(recipientId);
            var package = db.Packages.Find(packageId);

            var receipt = new Receipt
            {
                Id = Guid.NewGuid().ToString(),
                IssuedOn = DateTime.UtcNow,
                PackageId = package.Id,
                RecipientId = recipient.Id,
                Fee = (decimal)package.Weight * 2.67m
            };

            db.Receipts.Add(receipt);
            db.SaveChanges();

            return receipt.Id;
        }

        public IEnumerable<ReceiptViewModel> GetAllByUserId(string userId)
        {
            var receipts = db.Receipts
                .Where(x => x.RecipientId == userId)
                .Select(x => new ReceiptViewModel
                {
                    Id = x.Id,
                    Fee = x.Fee,
                    IssuedOn = x.IssuedOn.ToString(),
                    Recipient = x.Recipient.Username,
                })
                .ToList();

            return receipts;
        }
    }
}
