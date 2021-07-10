using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;

namespace cliente_cli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var client = new swaggerClient("https://localhost:5001", httpClient);

            var param = new LoginParameters()
            {
                UserName = "admin",
                Password = "Ab12345678"
            };
            var token = await client.SignInAsync(body: param);

            Console.WriteLine(token);

            httpClient.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", token);

            var u = await client.UsuarioAsync();
            Console.WriteLine(u.UserName);
        }
    }
}
