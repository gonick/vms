using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class DriverVehicleMessage
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Driver")]
        public int DriverId { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }

        [Required]
        public string message { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
