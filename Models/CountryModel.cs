using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAPI.Models
{
    public class CountryModel
    {
        public int? CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
