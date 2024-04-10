using Newtonsoft.Json;
using System.Net.Mime;

namespace BolcsessegBarkaAPITests
{
    [TestClass]
    public class BolcsessegBarkaAPITests
    {
        private static HttpClient? _client;

        [ClassInitialize]
        public static void Initialize(TestContext context) 
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:8881/api/");
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        }

        private static async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            var deserializedObject = JsonConvert.DeserializeObject<T>(responseString);
            return deserializedObject!;
        }
    }
}