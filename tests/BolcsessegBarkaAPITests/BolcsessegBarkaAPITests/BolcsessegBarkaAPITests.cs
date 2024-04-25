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
        
        [TestMethod]
        public async Task GetAllCourses_ReturnsUnauthorized() 
        {
            Unauthenticate();
            var response = await _client!.GetAsync("courses");
           
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllCourses_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("courses");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllCourses_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await _client!.GetAsync("courses");
            var courses = await Deserialize<CourseResponse>(response);
            Console.WriteLine(courses);
            Assert.IsNotNull(courses);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Történelem", courses.Data![0].Name);
        }
        
        [TestMethod]
        public async Task GetCourse_ReturnsUnauthorized()
        {
            Unauthenticate();
            var response = await _client!.GetAsync("courses/1");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetCourse_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("courses/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetCourse_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("courses/1");
            var course = await Deserialize<Course>(response);

            Assert.IsNotNull(course);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Történelem", course.Name);
        }
        
        [TestMethod]
        public async Task CreateCourse_ReturnsCreated()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var course = new StringContent(
                JsonConvert.SerializeObject(new { name = "TesztKurzus1", image="teszt.jpg", created_by="2" }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("courses", course);
            
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
        
        [TestMethod]
        public async Task CreateCourse_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var course = new StringContent(
                JsonConvert.SerializeObject(new { name = "TesztKurzus2", image="teszt.jpg", created_by="2" }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("courses", course);
            var repsonseData = await Deserialize<Course>(response);
            
            Assert.IsNotNull(course);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("TesztKurzus2", repsonseData.Name);
        }
        
        [TestMethod]
        public async Task UpdateCourse_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
           
            var course = new StringContent("{\"name\": \"TesztKurzus 2\", \"image\": \"asd.png\"}", null, "application/json");
            var response = await _client!.PutAsync("courses/3", course);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task UpdateCourse_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var course = new StringContent("{\"name\": \"TesztKurzus 3\", \"image\": \"asd.png\"}", null, "application/json");
            
            var response = await _client!.PutAsync("courses/3", course);
            var repsonseData = await Deserialize<Course>(response);
            
            Assert.IsNotNull(course);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("TesztKurzus 3", repsonseData.Name);
        }
        
        [TestMethod]
        public async Task DeleteCourse_ReturnsNoContent()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.DeleteAsync("courses/2");
            
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllGroups_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("groups");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllGroups_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("groups");
            var groups = await Deserialize<GroupResponse>(response);

            Assert.IsNotNull(groups);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, groups.Data![0].Id);
        }
        
        [TestMethod]
        public async Task GetGroup_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("groups/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetGroup_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("groups/1");
            var group = await Deserialize<Group>(response);

            Assert.IsNotNull(group);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, group.Id);
        }
        
        [TestMethod]
        public async Task CreateGroup_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var group = new StringContent(
                JsonConvert.SerializeObject(new { name = "Teszt Csoport" }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("groups", group);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task CreateGroup_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var group = new StringContent(
                JsonConvert.SerializeObject(new { name = "Teszt Csoport 2" }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("groups", group);
            var repsonseData = await Deserialize<Group>(response);
            
            Assert.IsNotNull(group);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Teszt Csoport 2", repsonseData.Name);
        }
        
        [TestMethod]
        public async Task UpdateGroup_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
           
            var group = new StringContent("{\"name\": \"Teszt Csoport 3\", \"selectedUsers\": [{\"id\": 2, \"permission\": \"Tanár\"}]}", null, "application/json");
            var response = await _client!.PutAsync("groups/1", group);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task UpdateGroup_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var group = new StringContent("{\"name\": \"Teszt Csoport 3\", \"selectedUsers\": [{\"id\": 2, \"permission\": \"Tanár\"}]}", null, "application/json");
            
            var response = await _client!.PutAsync("groups/1", group);
            var repsonseData = await Deserialize<Group>(response);
            
            Assert.IsNotNull(group);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(repsonseData.Users.Exists(x => x.Id == 2));
        }
        
        [TestMethod]
        public async Task DeleteGroup_ReturnsNoContent()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.DeleteAsync("groups/5");
            
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        
    }
}