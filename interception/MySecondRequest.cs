using System.Net.Http;
using System.Threading.Tasks;

namespace Example
{
    public class MySecondRequest : Request
    {
        private readonly HttpClient http;

        public MySecondRequest(HttpClient http)
        {
            this.http = http;
        }

        public string Response { get; private set; }

        public override async Task Get()
        {
            Response = await http.GetStringAsync("https://jsonmonk.com/api/v1/users?page=1");
        }

        public override void GetSync()
        {
            Response = http.GetStringAsync("https://jsonmonk.com/api/v1/users?page=1").GetAwaiter().GetResult();
        }
    }
}
