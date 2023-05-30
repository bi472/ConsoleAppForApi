using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft;

class Program
{
    static async Task Main()
    {
        
        using (HttpClient client = new HttpClient())
        {
            string text = "Это очень большой большой текст.";
            
            HttpContent content = new StringContent(text);

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await client.PostAsync("https://localhost:7049/Process", content);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Десериализуем JSON в словарь
                Dictionary<string, int> responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(jsonResponse);

                // Выводим полученные данные
                foreach (KeyValuePair<string, int> item in responseData)
                {
                    Console.WriteLine($"{item.Key}: {item.Value}");
                }
            }
            else
            {
                Console.WriteLine($"Ошибка: {response.StatusCode}");
            }
        }
    }
}
