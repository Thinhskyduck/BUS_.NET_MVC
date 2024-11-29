using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using TEDU.Areas.Admin.Models;
using Newtonsoft.Json;

namespace TEDU.Services
{
    public class NewsAPIService
    {
        private readonly HttpClient _httpClient;

        public NewsAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7210/api/");
        }

        //Lấy tuyến xe dựa vòa mã tin tức
        public async Task<News> GetNewByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"New/GetNew/{id}");

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var news = JsonConvert.DeserializeObject<News>(jsonResponse);

                return news;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching new by ID {id}: {ex.Message}");
                throw;
            }
        }

        //Lấy danh sách tin tức
        public async Task<List<News>> GetNewsAsync()
        {
            var response = await _httpClient.GetAsync("New/GetNews");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<News>>();
            }
            return null;
        }

        // Tạo tuyến xe mới
        public async Task<string> CreateNewAsync(News news)
        {
            var content = new StringContent(JsonConvert.SerializeObject(news), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("New/PostNew", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Cập nhật tuyến xe
        public async Task<string> UpdateNewAsync(int Id, News news)
        {
            var content = new StringContent(JsonConvert.SerializeObject(news), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"New/PutNew/{Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Update successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Xóa tuyến xe
        public async Task<string> DeleteNewAsync(int Id)
        {
            var response = await _httpClient.DeleteAsync($"New/DeleteNew/{Id}");
            if (response.IsSuccessStatusCode)
            {
                return "Delete successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

    }
}