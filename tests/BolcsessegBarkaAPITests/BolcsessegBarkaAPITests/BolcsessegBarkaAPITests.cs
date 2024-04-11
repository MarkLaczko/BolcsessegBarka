using ApiLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace BolcsessegBarkaAPITests
{
    [TestClass]
    public class BolcsessegBarkaAPITests
    {
        private static HttpClient? _client;
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void Initialize(TestContext context) 
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:8881/api/");
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);

            var jObject = JObject.Parse(responseString);

            var dataToken = jObject.SelectToken("data");
            if (dataToken != null)
            {
                try
                {
                    var dataDeserializedObject = dataToken.ToObject<T>();
                    if (dataDeserializedObject != null)
                    {
                        return dataDeserializedObject;
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error during deserialization of 'data': {ex.Message}");
                }
            }

            try
            {
                var directDeserializedObject = JsonConvert.DeserializeObject<T>(responseString);
                if (directDeserializedObject != null)
                {
                    return directDeserializedObject;
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error during direct deserialization: {ex.Message}");
            }

            return default(T)!;
        }

        public async Task<string> AuthenticateAndGetToken()
        {
            var loginUrl = "http://localhost:8881/api/users/login";
            var loginRequestBody = new
            {
                email = "admin@admin.com",
                password = "admin123"
            };

            var requestBody = new StringContent(JsonConvert.SerializeObject(loginRequestBody), Encoding.UTF8, "application/json");
            var response = await _client!.PostAsync(loginUrl, requestBody);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseString);
                if (tokenResponse?.Data?.Token != null)
                {
                    return tokenResponse.Data.Token;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public void Unauthenticate()
        {
            _client!.DefaultRequestHeaders.Authorization = null;
        }

        [TestMethod]
        public async Task GetAllUsers_ReturnsUnauthorized() 
        {
            Unauthenticate();
            var response = await _client!.GetAsync("users");
           
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task GetAllUsers_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("users");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetAllUsers_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await _client!.GetAsync("users");
            var users = await Deserialize<UserResponse>(response);

            Assert.IsNotNull(users);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("admin", users.Data![0].Name);
        }

        [TestMethod]
        public async Task GetUser_ReturnsUnauthorized()
        {
            Unauthenticate();
            var response = await _client!.GetAsync("users/1");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public async Task GetUser_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("users/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetUser_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("users/1");
            var user = await Deserialize<User>(response);

            Assert.IsNotNull(user);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("admin", user.Name);
        }
    }
}