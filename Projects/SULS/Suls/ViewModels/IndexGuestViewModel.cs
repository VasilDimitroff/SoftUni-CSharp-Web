using System;
using System.Globalization;

namespace Suls.ViewModels
{
    public class IndexGuestViewModel
    {
        public string Message => "Welcome to SULS Platform";
        public int Year => DateTime.Now.Year;
    }
}
