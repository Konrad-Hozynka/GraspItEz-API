using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraspItEz.Database
{
    public class QueryStatus
    {
        [Required]
        [Range(1, 3)]
        public int QueryStatusId { get; set; }
        [Required]
        public string QueryStatusValue { get; set; }
    }
}