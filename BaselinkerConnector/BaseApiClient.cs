using BaselinkerConnector.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BaselinkerConnector
{
    public class BaseApiClient
    {
        HttpClient client;
        private readonly string _token;
        private readonly string _api;
        public BaseApiClient()
        {
            client = new HttpClient();
            _token = ConfigurationManager.AppSettings.Get("BaselinkerToken");
            _api = ConfigurationManager.AppSettings.Get("BaselinkerApi");
        }

        protected async Task<Tres> CallApi<Tpar,Tres>(RequestMethod method, Tpar pars) where Tres:BaseResponse
        {
            var reqData = GetRequestData(method, pars);

            var response = await client.PostAsync(_api, reqData);

            var responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<Tres>(responseString);
            if (res.status == Status.ERROR)
            {
                throw new InvalidOperationException(
                    $"Error during Baselinker request. Error code:{res.error_code}, Error message:{res.error_message}");
            }
            return res;
        }

        private FormUrlEncodedContent GetRequestData<Tpar>(RequestMethod method, Tpar pars)
        {
            var parsSerialized = JsonConvert.SerializeObject(pars);
            var values = new Dictionary<string, string>
            {
                { "token", _token },
                { "method", method.ToString() },
                { "parameters", parsSerialized }
            };

            var content = new FormUrlEncodedContent(values);
            return content;
        }

    }
}
