using Panda.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Services
{
    public interface IReceiptsService
    {
        public string Create(string recipientId, string packageId);
        public IEnumerable<ReceiptViewModel> GetAllByUserId(string userId);
    }
}
