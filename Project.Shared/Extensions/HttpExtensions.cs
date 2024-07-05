using System.Net.Http.Headers;
using System.Text;

namespace Project.Shared.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<HttpContent> GetHttpContentAsync(this string requestBody)
        {
            var buffer = Encoding.UTF8.GetBytes(requestBody);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await Task.FromResult(byteContent);
        }

        public static async Task<string> GetContentMessageAsync(this HttpResponseMessage responseMessage)
        {
            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
