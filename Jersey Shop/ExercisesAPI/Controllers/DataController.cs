using ExercisesAPI.DAL;
using ExercisesAPI.DAL.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace ExercisesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        readonly AppDbContext _ctx;
        readonly IWebHostEnvironment _env;
        public DataController(AppDbContext context, IWebHostEnvironment env) // injected here
        {
            _ctx = context;
            _env = env;
        }
        [HttpGet]
        public async Task<ActionResult<String>> Index()
        {
            DataUtility util = new(_ctx);
            string payload;
            var json = await GetMenuItemJsonFromWebAsync();
            try
            {
                payload = (await util.LoadNutritionInfoFromWebToDb(json)) ? "tables loaded" : "problem loading tables";
            }
            catch (Exception ex)
            {
                payload = ex.Message;
            }
            return JsonSerializer.Serialize(payload);
        }
        private async static Task<String> GetMenuItemJsonFromWebAsync()
        {
            string url = "https://raw.githubusercontent.com/elauersen/info3067/master/mcdonalds.json";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        [Route("loadstores")]
        [HttpGet]
        public async Task<ActionResult<String>> LoadStores()
        {
            string payload;
            StoreDAO dao = new(_ctx);
            bool storesLoaded = await dao.LoadStoresFromFile(_env.WebRootPath);
            try
            {
                payload = storesLoaded ? "stores loaded successfully" : "problem loading store data";
            }
            catch (Exception ex)
            {
                payload = ex.Message;
            }
            return JsonSerializer.Serialize(payload);
        }
    }
}
