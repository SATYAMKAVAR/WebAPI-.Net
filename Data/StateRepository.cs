using System.Data.SqlClient;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class StateRepository
    {
        private readonly IConfiguration _configuration;

        #region Configuration
        public StateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        public List<StateModel> GetAllStates()
        {
            var StateList = new List<StateModel>();
            String ConnectionString = _configuration.GetConnectionString("ConnectionString");
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("PR_LOC_State_SelectAll", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var State = new StateModel
                            {
                                StateID = Convert.ToInt32(sdr["StateID"]),
                                CountryID = Convert.ToInt32(sdr["CountryID"]),
                                StateName = sdr["StateName"].ToString(),
                                StateCode = sdr["StateCode"].ToString(),
                                CreatedDate = Convert.ToDateTime(sdr["CreatedDate"]),
                                ModifiedDate = sdr["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(sdr["ModifiedDate"]) : DateTime.MinValue
                            };
                            StateList.Add(State);
                        }
                    }
                }
            }
            return StateList;
        }
    }
}
