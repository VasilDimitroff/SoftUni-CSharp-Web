using Panda.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panda.Controllers
{
    public class ReceiptsController : Controller
    {
        private readonly IReceiptsService receiptsService;

        public ReceiptsController(IReceiptsService receiptsService)
        {
            this.receiptsService = receiptsService;
        }

        public HttpResponse Index()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var receipts = receiptsService.GetAllByUserId(GetUserId());
            return this.View(receipts);
        }
    }
}
