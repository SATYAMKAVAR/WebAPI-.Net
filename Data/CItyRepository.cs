using System.Data;
using System.Data.SqlClient;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class CItyRepository
    {
        private readonly IConfiguration _configuration;

        #region Configuration
        public CItyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion


        public List<CityModel> GetAllCities()
        {
            var cityList = new List<CityModel>();

            // Retrieve the connection string from the configuration
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            // Use a using block to manage the SqlConnection
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Prepare the SqlCommand
                using (var objCmd = new SqlCommand("PR_LOC_City_SelectAll", conn))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    // Execute the command and process the SqlDataReader
                    using (var objSDR = objCmd.ExecuteReader())
                    {
                        while (objSDR.Read())
                        {
                            // Populate the CityModel object and add it to the list
                            var city = new CityModel
                            {
                                CityID = Convert.ToInt32(objSDR["CityID"]),          
                                CountryID = Convert.ToInt32(objSDR["CountryID"]),    
                                StateID = Convert.ToInt32(objSDR["StateID"]),        
                                CityName = objSDR["CityName"].ToString(),            
                                CityCode = objSDR["CityCode"].ToString(),            
                                CreatedDate = Convert.ToDateTime(objSDR["CreatedDate"]),
                                //ModifiedDate = Convert.ToDateTime(objSDR["ModifiedDate"]),
                                ModifiedDate = objSDR["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(objSDR["ModifiedDate"]) : DateTime.MinValue
                            };

                            cityList.Add(city);
                        }
                    }
                }
            }

            return cityList;
        }
    }
}
