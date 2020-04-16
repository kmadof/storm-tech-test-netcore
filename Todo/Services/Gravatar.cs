using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Todo.Services
{
    public static class Gravatar
    {
        private static readonly HttpClient client = new HttpClient();

        static Gravatar()
        {
            // Idally it would be to apply some kind of policy how to handle unavailable service
            // Polly can do the right job here
            client.Timeout = TimeSpan.FromSeconds(2);
        }

        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public static string GetUserName(string hash)
        {
            var gravatarProfileUrl = $"https://www.gravatar.com/{hash}.json";

            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("User-Agent", "Storm Ideas rules the world");

                HttpResponseMessage response = client.GetAsync(gravatarProfileUrl).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                GravatarProfile p = JsonConvert.DeserializeObject<GravatarProfile>(responseBody);

                return p.Entry.FirstOrDefault()?.DisplayName;
            }
            catch (HttpRequestException e)
            {
                // Log exception
            }

            return string.Empty;
        }
    }
}