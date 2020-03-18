using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Driver : BaseEntity
    {
        [Key]
        [Required]
        public int DriverId { get; set; }

        [MaxLength(100)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        [Required]
        public string DrivingLicenseNumber { get; set; }

        [MaxLength(3)]
        [Required]
        public string LicenseCountryCode { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
