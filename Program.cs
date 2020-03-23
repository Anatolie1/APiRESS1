using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Api1
{
    class Program
    {

        static async Task Main(string[] args)
        {
            List<Post> posts = new List<Post>();
            List<Comment> comments = new List<Comment>();
            List<Album> albums = new List<Album>();
            List<Photo> photos = new List<Photo>();
            

            Console.WriteLine("Give user Id (1 to 10)");
            int user = Convert.ToInt32(Console.ReadLine());

            var httpClient = HttpClientFactory.Create();
            var url = "https://jsonplaceholder.typicode.com/posts";
            var url1 = "https://jsonplaceholder.typicode.com/comments";
            var url2 = "https://jsonplaceholder.typicode.com/albums";
            var url3 = "https://jsonplaceholder.typicode.com/photos";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

            var content = httpResponseMessage.Content;
            var result = await content.ReadAsStringAsync();
            posts = JsonConvert.DeserializeObject<List<Post>>(result).ToList();
            //var userpost = myList.Where(x => x.UserId == user);
            HttpResponseMessage httpResponseMessage1 = await httpClient.GetAsync(url1);

            var content1 = httpResponseMessage1.Content;
            var result1 = await content1.ReadAsStringAsync();
            comments = JsonConvert.DeserializeObject<List<Comment>>(result1).ToList();

            HttpResponseMessage httpResponseMessage2 = await httpClient.GetAsync(url2);

            var content2 = httpResponseMessage2.Content;
            var result2 = await content2.ReadAsStringAsync();
            albums = JsonConvert.DeserializeObject<List<Album>>(result2).ToList();

            HttpResponseMessage httpResponseMessage3 = await httpClient.GetAsync(url3);

            var content3 = httpResponseMessage3.Content;
            var result3 = await content3.ReadAsStringAsync();
            photos = JsonConvert.DeserializeObject<List<Photo>>(result3).ToList();

            Console.WriteLine("Posts");

            posts.Where(x => x.UserId == user).ToList().ForEach(i => Console.WriteLine(i));
            var listcomments = posts.Where(x => x.UserId == user).Select(x => x.Id).ToList();

            Console.WriteLine("Comments");

            foreach (int item in listcomments)
            {
                comments.Where(c => c.PostId == item).ToList().ForEach(i => Console.WriteLine(i));
            }

            Console.WriteLine("Albums");

            albums.Where(x => x.UserId == user).ToList().ForEach(i => Console.WriteLine(i));
            var listalbums = albums.Where(x => x.UserId == user).Select(x => x.Id).ToList();

            Console.WriteLine("Photos");

            foreach (int item in listalbums)
            {
                photos.Where(c => c.AlbumId == item).ToList().ForEach(i => Console.WriteLine(i));
            }
        }
    }

    class Post
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("completed")]
        public string Body { get; set; }

        public override string ToString()
        {
            return $"{ UserId}, {Id}, {Title}, {Body}";
        }
    }

    class Comment
    {
        [JsonProperty("postId")]
        public int PostId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        public override string ToString()
        {
            return $"{ PostId}, {Id}, {Name}, {Email}, {Body}";
        }
    }
    class Album
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        public override string ToString()
        {
            return $"{ UserId}, {Id}, {Title}";
        }
    }
    class Photo
    {
        [JsonProperty("albumId")]
        public int AlbumId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        public override string ToString()
        {
            return $"{ AlbumId}, {Id}, {Title}, {Url}, {ThumbnailUrl}";
        }
    }
}