using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace TelegramBot.ArtificialIntelligence.Service;
public class ChatGptService : IAiService
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint = "https://api.openai.com/v1/chat/completions";
    private readonly string _apiKey = "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";

    public ChatGptService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
    }
    public async Task<string> GetChatResponse(string userMessage)
    {
        // Yaratilgan prompt – tizim va foydalanuvchi xabarlari
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                    new { role = "system", content = "Siz foydalanuvchiga aniq va qisqa javob berasiz." },
                    new { role = "user", content = userMessage }
                }
        };

        // JSON ga serializatsiya qilamiz
        var jsonBody = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        // API ga POST so'rov yuboramiz
        var response = await _httpClient.PostAsync(_endpoint, content);

        // Agar javob muvaffaqiyatsiz bo'lsa, xatolikni tashlaymiz
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        // Javobni dinamik tarzda deserializatsiya qilamiz
        dynamic result = JsonConvert.DeserializeObject(responseString);
        string chatResponse = result.choices[0].message.content;

        return chatResponse;
    }
}