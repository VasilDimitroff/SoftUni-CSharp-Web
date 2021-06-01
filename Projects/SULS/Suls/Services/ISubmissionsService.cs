using System;
using System.Collections.Generic;
using System.Text;

namespace Suls.Services
{
    public interface ISubmissionsService
    {
        public string Create(string problemId, string code, string userId);
        public string Delete(string id);
    }
}
