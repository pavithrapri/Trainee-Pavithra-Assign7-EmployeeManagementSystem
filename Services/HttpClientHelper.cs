using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Helpers
{
    public static class HttpClientHelper
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task<string> MakePostRequest(string baseUrl, string endpoint, string apiRequestData)
        {
            HttpClient.BaseAddress = new Uri(baseUrl);
            StringContent apiRequestContent = new StringContent(apiRequestData, Encoding.UTF8, "application/json");
            var httpResponse = await HttpClient.PostAsync(endpoint, apiRequestContent);
            var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponseString);
            }
            return httpResponseString;
        }

        public static async Task<string> MakeGetRequest(string baseUrl, string endpoint)
        {
            HttpClient.BaseAddress = new Uri(baseUrl);
            var httpResponse = await HttpClient.GetAsync(endpoint);
            var httpResponseString = await httpResponse.Content.ReadAsStringAsync();
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponseString);
            }
            return httpResponseString;
        }
    }
}
