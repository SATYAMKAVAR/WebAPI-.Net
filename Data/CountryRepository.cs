using Microsoft.Data.SqlClient;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class CountryRepository
    {
        private readonly IConfiguration _configuration;

        #region Configuration
        public CountryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        public List<CountryModel> GetAllCountries()
        {
            var CountryList = new List<CountryModel>();

            String ConnectionString = _configuration.GetConnectionString("ConnectionString");
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("PR_LOC_Country_SelectAll", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var country = new CountryModel()
                            {
                                CountryID = Convert.ToInt32(sdr["CountryID"]),
                                CountryName = sdr["CountryName"].ToString(),
                                CountryCode = sdr["CountryCode"].ToString(),
                                CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]),
                                ModifiedDate = sdr["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(sdr["ModifiedDate"]) : DateTime.MinValue
                            };
                            CountryList.Add(country);
                        }
                    }
                }
            }


            return CountryList;
        }
    }
}
