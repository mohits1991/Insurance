using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Bll.Services.Interfaces
{
    /// <summary>
    /// Client request logic
    /// </summary>
    public class RequestService : IRequestService
    {
        private readonly HttpClient _client;
        public RequestService(string baseAddress)
        {
            _client = new HttpClient { BaseAddress = new Uri(baseAddress) };
        }

        /// <summary>
        /// Get call for specific endpoint
        /// </summary>
        /// <param name="endPointUri"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string endPointUri)
        {
           return await _client.GetAsync(endPointUri).Result.Content.ReadAsStringAsync();
        }
    }
}
