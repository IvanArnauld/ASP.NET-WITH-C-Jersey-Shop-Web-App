using ExercisesAPI.DAL.DomainClasses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExercisesAPI.DAL.DAO
{
    public class StoreDAO
    {
        private AppDbContext _db;
        public StoreDAO(AppDbContext context)
        {
            _db = context;
        }
        public async Task<bool> LoadStoresFromFile(string path)
        {
            bool addWorked = false;
            try
            {
                // clear out the old rows
                _db.Stores?.RemoveRange(_db.Stores);
                await _db.SaveChangesAsync();
                var csv = new List<string[]>();
                var csvFile = path + "\\exercisesStoreRaw.txt";
                var lines = await System.IO.File.ReadAllLinesAsync(csvFile);
                foreach (string line in lines)
                    csv.Add(line.Split(',')); // populate store with csv
                foreach (string[] rawdata in csv)
                {
                    Store aStore = new();
                    aStore.Longitude = Convert.ToDouble(rawdata[0]);
                    aStore.Latitude = Convert.ToDouble(rawdata[1]);
                    aStore.Street = rawdata[2];
                    aStore.City = rawdata[3];
                    aStore.Region = rawdata[4];
                    await _db.Stores!.AddAsync(aStore);
                    await _db.SaveChangesAsync();
                }
                addWorked = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return addWorked;
        }
        public async Task<List<Store>?> GetThreeClosestStores(float? lat, float? lon)
        {
            List<Store>? storeDetails = null;
            try
            {
                var latParam = new SqlParameter("@lat", lat);
                var lonParam = new SqlParameter("@lon", lon);
                var query = _db.Stores?.FromSqlRaw("dbo.pGetThreeClosestStores @lat, @lon", latParam,
                lonParam);
                storeDetails = await query!.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return storeDetails;
        }

    }
}