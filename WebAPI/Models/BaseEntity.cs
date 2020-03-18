using System;
namespace WebAPI.Models
{
    //base entity class to set a common record creation and deletion approach
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
