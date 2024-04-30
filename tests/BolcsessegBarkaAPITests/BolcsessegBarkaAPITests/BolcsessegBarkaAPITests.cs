using ApiLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using Group = ApiLibrary.Group;
using Task = System.Threading.Tasks.Task;
using TaskApi = ApiLibrary.Task;

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
        public async Task CreateUser_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var user = new StringContent(
                JsonConvert.SerializeObject(new { name="teszt", email = "teszt@teszt.com", password="teszt1234", password_confirmation="teszt1234" }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("users/register", user);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task CreateUser_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var user = new StringContent(
                JsonConvert.SerializeObject(new { name="teszt2", email = "teszt2@teszt.com", password="teszt1234", password_confirmation="teszt1234" }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("users/register", user);
            var responseMessage = await Deserialize<MessageResponse>(response);
            
            Assert.IsNotNull(user);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Successful registration",responseMessage.Message);
        }
        
        [TestMethod]
        public async Task UpdateUser_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
           
            var user = new StringContent("{\"name\": \"teszt3\", \"email\": \"teszt3@teszt.com\", \"password\": \"teszt1234\", \"password_confirmation\": \"teszt1234\" }", null, "application/json");
            var response = await _client!.PutAsync("users/4", user);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task UpdateUser_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var user = new StringContent("{\"name\": \"teszt4\", \"email\": \"teszt4@teszt.com\", \"password\": \"teszt1234\", \"password_confirmation\": \"teszt1234\" }", null, "application/json");
            
            var response = await _client!.PutAsync("users/4", user);
            var responseData = await Deserialize<User>(response);
            
            Assert.IsNotNull(user);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("teszt4", responseData.Name);
        }
        
        [TestMethod]
        public async Task DeleteUser_ReturnsNoContent()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.DeleteAsync("users/4");
            
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        
        [TestMethod]
        public async Task DeleteMultipleUser_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var user = new StringContent(
                JsonConvert.SerializeObject(new { userIds = new int[] {2, 3} }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("users/delete", user);
            var responseMessage = await Deserialize<MessageResponse>(response);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Users deleted successfully", responseMessage.Message);
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
            var responseData = await Deserialize<Course>(response);
            
            Assert.IsNotNull(course);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("TesztKurzus2", responseData.Name);
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
            var responseData = await Deserialize<Course>(response);
            
            Assert.IsNotNull(course);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("TesztKurzus 3", responseData.Name);
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
        public async Task DeleteMultipleCourse_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var createCourse = new StringContent(
                JsonConvert.SerializeObject(new { name = "TesztKurzus4", image="teszt.jpg", created_by="1" }),
                Encoding.UTF8,
                "application/json");
            
            await _client!.PostAsync("courses", createCourse);
            
            var course = new StringContent(
                JsonConvert.SerializeObject(new { courseIds = new int[] {3, 4} }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("courses/delete", course);
            var responseMessage = await Deserialize<MessageResponse>(response);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Courses deleted successfully", responseMessage.Message);
        }

        [TestMethod]
        public async Task AssignGroupToCourse_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var groups = new StringContent(
                JsonConvert.SerializeObject(new { group_ids = new int[] {1,2} }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("courses/1/groups", groups);
            var responseData = await Deserialize<MessageResponse>(response);
            
            Assert.IsNotNull(groups);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Groups successfully assigned to the course.", responseData.Message);
        }
        
        [TestMethod]
        public async Task AssignTopicToCourse_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
            var topics = new StringContent(
                JsonConvert.SerializeObject(new { topic_ids = new int[] { 1 } }),
                Encoding.UTF8,
                "application/json");

            var response = await _client!.PostAsync("courses/1/topics", topics);
                
            Assert.IsNotNull(topics);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task ShowCourseWithUsers_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("courses/1/with-users");
            var course = await Deserialize<GroupResponse>(response);

            Assert.IsNotNull(course);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
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
            var responseData = await Deserialize<Group>(response);
            
            Assert.IsNotNull(group);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Teszt Csoport 2", responseData.Name);
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
            var responseData = await Deserialize<Group>(response);
            
            Assert.IsNotNull(group);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(responseData.Users.Exists(x => x.Id == 2));
        }
        
        [TestMethod]
        public async Task DeleteGroup_ReturnsNoContent()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.DeleteAsync("groups/5");
            
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TestMethod]
        public async Task CreateNote_ReturnsCreated()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var note = new StringContent(
                JsonConvert.SerializeObject(new { title = "Jegyzet", text = "Ez egy teszt jegyszet.", user_id = 1, topic_id = 1 }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("notes", note);
            
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
        
        [TestMethod]
        public async Task CreateNote_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var note = new StringContent(
                JsonConvert.SerializeObject(new { title = "Jegyzet2", text = "Ez egy teszt jegyszet.", user_id = 1, topic_id = 1 }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("notes", note);
            var responseData = await Deserialize<Note>(response);
            
            Assert.IsNotNull(note);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("Jegyzet2", responseData.Title);
        }
        
        [TestMethod]
        public async Task GetAllNotes_ReturnsUnauthorized() 
        {
            Unauthenticate();
            var response = await _client!.GetAsync("notes");
           
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllNotes_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("notes");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllNotes_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("notes");
            var notes = await Deserialize<NoteResponse>(response);

            Assert.IsNotNull(notes);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Jegyzet", notes.Data[0].Title);
        }
        
        [TestMethod]
        public async Task GetNote_ReturnsUnauthorized() 
        {
            Unauthenticate();
            var response = await _client!.GetAsync("notes/1");
           
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetNote_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("notes/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetNote_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("notes/1");
            var note = await Deserialize<Note>(response);

            Assert.IsNotNull(note);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Jegyzet", note.Title);
        }
        
        [TestMethod]
        public async Task UpdateNote_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
           
            var note = new StringContent("{\"title\": \"ÚjJegyzet\", \"text\": \"Teszt jegyzet.\", \"user_id\": 1, \"topic_id\": 1}", null, "application/json");
            var response = await _client!.PutAsync("notes/2", note);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task UpdateNote_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var note = new StringContent("{\"title\": \"ÚjJegyzet2\", \"text\": \"Teszt jegyzet.\", \"user_id\": 1, \"topic_id\": 1}", null, "application/json");
            
            var response = await _client!.PutAsync("notes/2", note);
            var responseData = await Deserialize<Note>(response);
            
            Assert.IsNotNull(note);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("ÚjJegyzet2", responseData.Title);
        }
        
        [TestMethod]
        public async Task DeleteNote_ReturnsNoContent()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.DeleteAsync("notes/2");
            
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        
        [TestMethod]
        public async Task CreateTopic_ReturnsCreated()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var topic = new StringContent(
                JsonConvert.SerializeObject(new { name = "Téma", course_id = 1}),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("topics", topic);
            
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
        
        [TestMethod]
        public async Task CreateTopic_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var topic = new StringContent(
                JsonConvert.SerializeObject(new { name = "Téma2", course_id = 1 }),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("topics", topic);
            var responseData = await Deserialize<Topic>(response);
            
            Assert.IsNotNull(topic);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("Téma2", responseData.Name);
        }
        
        [TestMethod]
        public async Task GetAllTopics_ReturnsUnauthorized() 
        {
            Unauthenticate();
            var response = await _client!.GetAsync("topics");
           
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllTopics_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("topics");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllTopics_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("topics");
            var topics = await Deserialize<TopicResponse>(response);

            Assert.IsNotNull(topics);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Történelmi korszakok", topics.Data[0].Name);
        }
        
        [TestMethod]
        public async Task GetTopic_ReturnsUnauthorized() 
        {
            Unauthenticate();
            var response = await _client!.GetAsync("topics/1");
           
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetTopic_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("topics/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetTopic_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("topics/1");
            var topic = await Deserialize<Topic>(response);

            Assert.IsNotNull(topic);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Történelmi korszakok", topic.Name);
        }
        
        [TestMethod]
        public async Task UpdateTopic_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
           
            var topic = new StringContent("{\"name\": \"Téma3\"}", null, "application/json");
            var response = await _client!.PutAsync("topics/3", topic);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task UpdateTopic_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var topic = new StringContent("{\"name\": \"Téma4\"}", null, "application/json");
            
            var response = await _client!.PutAsync("topics/3", topic);
            var responseData = await Deserialize<Topic>(response);
            
            Assert.IsNotNull(topic);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Téma4", responseData.Name);
        }
        
        [TestMethod]
        public async Task DeleteTopic_ReturnsNoContent()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.DeleteAsync("topics/3");
            
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllQuizzes_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("quizzes");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetAllQuizzes_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("quizzes");
            var quizzes = await Deserialize<QuizResponse>(response);
            
            Assert.IsNotNull(quizzes);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("2023 május emelt", quizzes.Data[0].Name);
            Assert.IsNull(quizzes.Data[0].MaxAttempts);
            Assert.IsNull(quizzes.Data[0].Opens);
            Assert.IsNull(quizzes.Data[0].Closes);
            Assert.IsNull(quizzes.Data[0].Time);
        }
        
        [TestMethod]
        public async Task GetQuiz_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync("quizzes/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetQuiz_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client!.GetAsync("quizzes/1");
            var quiz = await Deserialize<QuizTopicCourse>(response);

            Assert.IsNotNull(quiz);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("2023 május emelt", quiz.Name);
            Assert.IsNull(quiz.MaxAttempts);
            Assert.IsNull(quiz.Opens);
            Assert.IsNull(quiz.Closes);
            Assert.IsNull(quiz.Time);
            Assert.AreEqual(1, quiz.NumberOfTasks);
            Assert.AreEqual(1, quiz.Topic.Id);
            Assert.AreEqual(1, quiz.Topic.Course.Id);
        }
        
        [TestMethod]
        public async Task CreateQuiz_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var quiz = new StringContent(
                JsonConvert.SerializeObject(new { name = "Teszt Kvíz", max_attempts = 1, opens = 1704063600, closes = 1735686000, time = 60, topic_id = 1}),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("quizzes", quiz);
            
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
        
        [TestMethod]
        public async Task CreateQuiz_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var quiz = new StringContent(
                JsonConvert.SerializeObject(new { name = "Teszt Kvíz", max_attempts = 1, opens = 1704063600, closes = 1735686000, time = 60, topic_id = 1}),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PostAsync("quizzes", quiz);
            var responseData = await Deserialize<Quiz>(response);
            
            Assert.IsNotNull(responseData);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual("Teszt Kvíz", responseData.Name);
            Assert.AreEqual(1 ,responseData.MaxAttempts);
            Assert.AreEqual(1704063600, responseData.Opens);
            Assert.AreEqual(1735686000, responseData.Closes);
            Assert.AreEqual(60, responseData.Time);
        }
        
        [TestMethod]
        public async Task UpdateQuiz_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var quiz = new StringContent(
                JsonConvert.SerializeObject(new { name = "Teszt Kvíz Frissítés", max_attempts = 1, opens = 1704063600, closes = 1735686000, time = 60, topic_id = 1}),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PutAsync("quizzes/1", quiz);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task UpdateQuiz_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var quiz = new StringContent(
                JsonConvert.SerializeObject(new { name = "Teszt Kvíz Frissítés", max_attempts = 1, opens = 1704063600, closes = 1735686000, time = 60, topic_id = 1}),
                Encoding.UTF8,
                "application/json");
            
            var response = await _client!.PutAsync("quizzes/1", quiz);
            var responseData = await Deserialize<Quiz>(response);
            
            Assert.IsNotNull(responseData);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Teszt Kvíz Frissítés", responseData.Name);
            Assert.AreEqual(1 ,responseData.MaxAttempts);
            Assert.AreEqual(1704063600, responseData.Opens);
            Assert.AreEqual(1735686000, responseData.Closes);
            Assert.AreEqual(60, responseData.Time);
        }
        
        [TestMethod]
        public async Task DeleteQuiz_ReturnsNoContent()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var quiz = new StringContent(
                JsonConvert.SerializeObject(new { name = "Teszt Kvíz", max_attempts = 1, opens = 1704063600, closes = 1735686000, time = 60, topic_id = 1}),
                Encoding.UTF8,
                "application/json");
            
            var postResponse = await _client!.PostAsync("quizzes", quiz);
            var postResponseData = await Deserialize<Quiz>(postResponse);
            
            var response = await _client!.DeleteAsync($"quizzes/{postResponseData.Id}");
            
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetQuizTasks_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync($"quizzes/1/tasks");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetQuizTasks_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync($"quizzes/1/tasks");
            var tasks = await Deserialize<TaskResponse>(response);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, tasks.Data[0].Id);
            Assert.AreEqual("A feladat a földrajzi felfedezések következményeihez kapcsolódik.", tasks.Data[0].Header);
            Assert.AreEqual(1, tasks.Data[0].Subtasks[0].Id);
        }
        
        [TestMethod]
        public async Task GetQuizTasksIds_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync($"quizzes/1/tasks/ids");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetQuizTasksIds_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync($"quizzes/1/tasks/ids");
            var tasks = await Deserialize<List<int>>(response);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, tasks[0]);
        }
        
        [TestMethod]
        public async Task GetTask_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync($"tasks/1");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetTask_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync($"tasks/1");
            var task = await Deserialize<TaskApi>(response);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, task.Id);
            Assert.AreEqual("A feladat a földrajzi felfedezések következményeihez kapcsolódik.", task.Header);
            Assert.AreEqual(1, task.Subtasks[0].Id);
        }
        
        [TestMethod]
        public async Task GetTaskSolution_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync($"tasks/1/solution");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetTaskSolution_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var response = await _client!.GetAsync($"tasks/1/solution");
            var solutions = await Deserialize<TaskSolutionResponse>(response);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(1, solutions.Data[0].Id);
            Assert.AreEqual("Amerika felfedezése", solutions.Data[0].Solution[0]);
            Assert.AreEqual("Kolumbusz Kristóf eljutott Amerikába", solutions.Data[0].Solution[1]);
        }
        
        [TestMethod]
        public async Task CreateTask_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var task = new StringContent("{\"quiz_id\": 1,\"order\": 7,\"header\": \"Teszt Kérdés\",\"text\": \"Ez egy teszt kvíz\",\"subtasks\": [{\"order\": 0,\"question\": \"Ez egy példa kérdés\",\"options\": [\"Igen\",\"Nem\",\"Talán\"],\"solution\": [\"Igen\"],\"type\": \"multiple_choice\",\"marks\": \"1\"}]}", null, "application/json");
            
            var response = await _client!.PostAsync("tasks", task);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task CreateTask_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var task = new StringContent("{\"quiz_id\": 1,\"order\": 7,\"header\": \"Teszt Kérdés\",\"text\": \"Ez egy teszt kvíz\",\"subtasks\": [{\"order\": 0,\"question\": \"Ez egy példa kérdés\",\"options\": [\"Igen\",\"Nem\",\"Talán\"],\"solution\": [\"Igen\"],\"type\": \"multiple_choice\",\"marks\": \"1\"}]}", null, "application/json");
            
            var response = await _client!.PostAsync("tasks", task);
            var responseData = await Deserialize<TaskApi>(response);
            
            Assert.IsNotNull(responseData);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Teszt Kérdés", responseData.Header);
            Assert.AreEqual("Ez egy teszt kvíz" ,responseData.Text);
            Assert.AreEqual("Ez egy példa kérdés", responseData.Subtasks[0].Question);
        }
        
        [TestMethod]
        public async Task UpdateTask_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var task = new StringContent("{\"quiz_id\": 1,\"order\": 7,\"header\": \"Teszt Kérdés\",\"text\": \"Ez egy teszt kvíz\",\"subtasks\": [{\"id\": 1, \"order\": 0,\"question\": \"Ez egy példa kérdés\",\"options\": [\"Igen\",\"Nem\",\"Talán\"],\"solution\": [\"Igen\"],\"type\": \"multiple_choice\",\"marks\": \"1\"}]}", null, "application/json");
            
            var response = await _client!.PutAsync("tasks/1", task);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task UpdateTask_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var task = new StringContent("{\"quiz_id\": 1,\"order\": 7,\"header\": \"Teszt Kérdés\",\"text\": \"Ez egy teszt kvíz\",\"subtasks\": [{\"id\": 1, \"order\": 0,\"question\": \"Ez egy példa kérdés\",\"options\": [\"Igen\",\"Nem\",\"Talán\"],\"solution\": [\"Igen\"],\"type\": \"multiple_choice\",\"marks\": \"1\"}]}", null, "application/json");
            
            var response = await _client!.PutAsync("tasks/1", task);
            var responseData = await Deserialize<TaskApi>(response);
            
            Assert.IsNotNull(responseData);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Teszt Kérdés", responseData.Header);
            Assert.AreEqual("Ez egy teszt kvíz" ,responseData.Text);
            Assert.AreEqual("Ez egy példa kérdés", responseData.Subtasks[0].Question);
        }
        
        [TestMethod]
        public async Task DeleteTask_ReturnsNoContent()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var task = new StringContent("{\"quiz_id\": 1,\"order\": 7,\"header\": \"Teszt Kérdés\",\"text\": \"Ez egy teszt kvíz\",\"subtasks\": [{\"order\": 0,\"question\": \"Ez egy példa kérdés\",\"options\": [\"Igen\",\"Nem\",\"Talán\"],\"solution\": [\"Igen\"],\"type\": \"multiple_choice\",\"marks\": \"1\"}]}", null, "application/json");
            
            var postResponse = await _client!.PostAsync("tasks", task);
            var postResponseData = await Deserialize<TaskApi>(postResponse);
            
            var response = await _client!.DeleteAsync($"tasks/{postResponseData.Id}");
            
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetSubtask_ReturnsOK()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var task = new StringContent("{\"quiz_id\": 1,\"order\": 7,\"header\": \"Teszt Kérdés\",\"text\": \"Ez egy teszt kvíz\",\"subtasks\": [{\"order\": 0,\"question\": \"Ez egy példa kérdés\",\"options\": [\"Igen\",\"Nem\",\"Talán\"],\"solution\": [\"Igen\"],\"type\": \"multiple_choice\",\"marks\": \"1\"}]}", null, "application/json");
            
            var postResponse = await _client!.PostAsync("tasks", task);
            var postResponseData = await Deserialize<TaskApi>(postResponse);
            
            var response = await _client!.GetAsync($"subtasks/{postResponseData.Subtasks[0].Id}/solution");
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        
        [TestMethod]
        public async Task GetSubtask_ReturnsValidData()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var task = new StringContent("{\"quiz_id\": 1,\"order\": 7,\"header\": \"Teszt Kérdés\",\"text\": \"Ez egy teszt kvíz\",\"subtasks\": [{\"order\": 0,\"question\": \"Ez egy példa kérdés\",\"options\": [\"Igen\",\"Nem\",\"Talán\"],\"solution\": [\"Igen\"],\"type\": \"multiple_choice\",\"marks\": \"1\"}]}", null, "application/json");
            
            var postResponse = await _client!.PostAsync("tasks", task);
            var postResponseData = await Deserialize<TaskApi>(postResponse);
            
            var response = await _client!.GetAsync($"subtasks/{postResponseData.Subtasks[0].Id}/solution");
            var responseData = await Deserialize<TaskSolution>(response);
            
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(postResponseData.Subtasks[0].Id, responseData.Id);
            Assert.AreEqual("Igen", responseData.Solution[0]);
        }
        
        [TestMethod]
        public async Task DeleteSubtask_ReturnsNoContent()
        {
            var token = await AuthenticateAndGetToken();
            _client!.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            
            var task = new StringContent("{\"quiz_id\": 1,\"order\": 7,\"header\": \"Teszt Kérdés\",\"text\": \"Ez egy teszt kvíz\",\"subtasks\": [{\"order\": 0,\"question\": \"Ez egy példa kérdés\",\"options\": [\"Igen\",\"Nem\",\"Talán\"],\"solution\": [\"Igen\"],\"type\": \"multiple_choice\",\"marks\": \"1\"}]}", null, "application/json");
            
            var postResponse = await _client!.PostAsync("tasks", task);
            var postResponseData = await Deserialize<TaskApi>(postResponse);
            
            var response = await _client!.DeleteAsync($"subtasks/{postResponseData.Subtasks[0].Id}");
            
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}