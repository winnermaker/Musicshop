using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Customer
{
    public static class ServiceClient
    {
        internal async static Task<List<string>> GetCategoryNamesAsync()
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<string>>
                (await lcHttpClient.GetStringAsync("http://localhost:60065/api/shop/GetCategoryNames/"));
        }
        internal async static Task<clsCategory> GetCategoryAsync(string prCategoryName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsCategory>
                (await lcHttpClient.GetStringAsync
                ("http://localhost:60065/api/shop/GetCategory?CategoryName=" + prCategoryName));
        }
        internal async static Task<clsAllInstruments> GetInstrumentAsync(int prSerialNo)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsAllInstruments>
                (await lcHttpClient.GetStringAsync
                ("http://localhost:60065/api/shop/GetInstrument?SerialNo=" + prSerialNo));
        }

        internal static async Task<string> InsertInstrumentAsync(clsAllInstruments prInstrument)
        {
            return await InsertOrUpdateAsync(prInstrument, "http://localhost:60065/api/shop/PostInstrument", "POST");
        }
        internal static async Task<string> UpdateInstrumentAsync(clsAllInstruments prInstrument)
        {
            return await InsertOrUpdateAsync(prInstrument, "http://localhost:60065/api/shop/PutInstrument", "PUT");
        }
        internal static async Task<string> UpdateInstrumentTestAsync(clsAllInstruments prInstrument)
        {
            return await InsertOrUpdateAsync(prInstrument, "http://localhost:60065/api/shop/PutInstrumentTest", "PUT");
        }

        internal static async Task<string> DeleteInstrumentAsync(clsAllInstruments prInstrument)
        {
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.DeleteAsync
                ($"http://localhost:60065/api/shop/DeleteInstrument?SerialNo={prInstrument.SerialNo}");
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }

        internal static async Task<string> UpdateCategoryAsync(clsCategory prCategory)
        {
            return await InsertOrUpdateAsync(prCategory, "http://localhost:60065/api/shop/PutCategory", "PUT");
        }


        internal static async Task<string> InsertOrderAsync(clsMyOrder prOrder)
        {
            return await InsertOrUpdateAsync(prOrder, "http://localhost:60065/api/shop/PostOrder", "POST");
        }

        private async static Task<string> InsertOrUpdateAsync<TItem>(TItem prItem, string prUrl, string prRequest)
        {
            using (HttpRequestMessage lcReqMessage = new HttpRequestMessage(new HttpMethod(prRequest), prUrl))
            using (lcReqMessage.Content =
            new StringContent(JsonConvert.SerializeObject(prItem), Encoding.UTF8, "application/json"))
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.SendAsync(lcReqMessage);
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }
    }
}
