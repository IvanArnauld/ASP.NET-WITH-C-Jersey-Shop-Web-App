using ExercisesAPI.DAL.DomainClasses;
using System.Text.Json;
namespace ExercisesAPI.DAL
{
    public class DataUtility
    {
        private readonly AppDbContext _db;
        public DataUtility(AppDbContext context)
        {
            _db = context;
        }
        public async Task<bool> LoadNutritionInfoFromWebToDb(string stringJson)
        {
            bool categoriesLoaded = false;
            bool menuItemsLoaded = false;
            try
            {
                // an element that is typed as dynamic is assumed to support any operation
                dynamic? objectJson = JsonSerializer.Deserialize<Object>(stringJson);
                categoriesLoaded = await LoadCategories(objectJson);
                menuItemsLoaded = await LoadMenuItems(objectJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return categoriesLoaded && menuItemsLoaded;
        }

        private async Task<bool> LoadCategories(dynamic jsonObjectArray)
        {
            bool loadedCategories = false;
            try
            {
                // clear out the old rows
                _db.Categories?.RemoveRange(_db.Categories);
                await _db.SaveChangesAsync();
                List<String> allCategories = new();
                foreach (JsonElement element in jsonObjectArray.EnumerateArray())
                {
                    if (element.TryGetProperty("CATEGORY", out JsonElement menuItemJson))
                    {
                        allCategories.Add(menuItemJson.GetString()!);
                    }
                }
                IEnumerable<String> categories = allCategories.Distinct<String>();
                foreach (string catname in categories)
                {
                    Category cat = new();
                    cat.Name = catname;
                    await _db.Categories!.AddAsync(cat);
                    await _db.SaveChangesAsync();
                }
                loadedCategories = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadedCategories;
        }
        private async Task<bool> LoadMenuItems(dynamic jsonObjectArray)
        {
            bool loadedItems = false;
            try
            {
                List<Category> categories = _db.Categories!.ToList();
                // clear outthe old
                _db.MenuItems?.RemoveRange(_db.MenuItems);
                await _db.SaveChangesAsync();
                foreach (JsonElement element in jsonObjectArray.EnumerateArray())
                {
                    MenuItem item = new();
                    item.Calories = Convert.ToInt32(element.GetProperty("CAL").GetString());
                    item.Carbs = Convert.ToInt32(element.GetProperty("CARB").GetString());
                    item.Cholesterol = Convert.ToInt32(element.GetProperty("CHOL").GetString());
                    item.Fat = Convert.ToSingle(element.GetProperty("FAT").GetString());
                    item.Fibre = Convert.ToInt32(element.GetProperty("FBR").GetString());
                    item.Protein = Convert.ToInt32(element.GetProperty("PRO").GetString());
                    item.Salt = Convert.ToInt32(element.GetProperty("SALT").GetString());
                    item.Description = element.GetProperty("ITEM").GetString();
                    string? cat = element.GetProperty("CATEGORY").GetString();
                    // add the FK here
                    foreach (Category category in categories)
                    {
                        if (category.Name == cat)
                        {
                            item.Category = category;
                            break;
                        }
                    }
                    await _db.MenuItems!.AddAsync(item);
                    await _db.SaveChangesAsync();
                }
                loadedItems = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadedItems;
        }
    }
}