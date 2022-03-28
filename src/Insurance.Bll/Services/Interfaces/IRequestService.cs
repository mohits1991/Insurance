using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Bll.Services.Interfaces
{
    public interface IRequestService
    {
        Task<string> GetAsync(string endPointUri);
    }
}
