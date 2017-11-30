using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;

namespace ApiCheck
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public Picture Picture { get; set; }
    }

    public class Picture
    {
        public string Name { get; set; }
        public string MIME { get; set; }
        public string PathToPicture { get; set; }
    }

    class Program
    {

        static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"host.txt");
        static string port = File.ReadAllText(path);

        private static string APP_PATH = "http://localhost:" + port;//"55812"
        private static string token;

        static void Main(string[] args)
        {
            //Console.WriteLine(APP_PATH);

            Console.WriteLine("Login(hit Enter if john@mail.com): ");
            string inputVal = Console.ReadLine();

            string userName = String.IsNullOrEmpty(inputVal) ? "john@mail.com" : inputVal;

            Console.WriteLine("Password(hit Enter if pas123): ");
            inputVal = Console.ReadLine();

            string password = String.IsNullOrEmpty(inputVal) ? "pas123" : inputVal;

            Dictionary<string, string> tokenDictionary = GetTokenDictionary(userName, password);
            token = tokenDictionary["access_token"];

            string values = GetValues(token);
            Note[] notes = JsonConvert.DeserializeObject<Note[]>(values);

            foreach (Note note in notes)
            {
                Console.WriteLine("Notes:\n");
                Console.WriteLine("Note id: {0}\n", note.Id);
                Console.WriteLine("Creation time: {0}\n", note.CreationTime);
                Console.WriteLine("Title: {0}\n", note.Title);
                Console.WriteLine("Text: {0}\n", note.Text);
                Console.WriteLine("Picture :\n");
                if (note.Picture != null)
                {
                    Console.WriteLine("\t\tName: {0}\n", note.Picture.Name);
                    Console.WriteLine("\t\tMIME: {0}\n", note.Picture.MIME);
                    Console.WriteLine("\t\tPath to picture: {0}\n", "http://localhost:55812/" + note.Picture.PathToPicture);
                }
                else
                {
                    Console.WriteLine("\t\tnull\n");
                }
                Console.WriteLine(new String('/', 20));
            }
            Console.Read();
        }

        static Dictionary<string, string> GetTokenDictionary(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", userName ),
                    new KeyValuePair<string, string> ( "Password", password )
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                var response =
                    client.PostAsync(APP_PATH + "/Token", content).Result;
                var result = response.Content.ReadAsStringAsync().Result;

                Dictionary<string, string> tokenDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                return tokenDictionary;
            }
        }

        static HttpClient CreateClient(string accessToken = "")
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return client;
        }

        static string GetValues(string token)
        {
            using (var client = CreateClient(token))
            {
                var response = client.GetAsync(APP_PATH + "/api/values").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
