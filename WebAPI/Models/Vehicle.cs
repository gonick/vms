using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace WebAPI.Models
{
    public enum VehicleType
    {
        Truck,
        Van
    }
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }


    public class Vehicle : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public VehicleType Type { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string LicenseNumber { get; set; }

        public float Speed { get; set; } //in kmph

        public double Latitude { get; set; }

        public double Longitude { get; set; }


        public float Temperature { get; set; } //in celcius

        [DefaultValue("false")]
        public bool isDeleted { get; set; }

    }
}
//assumptions:
// all data is collected form a central unit
// and alll vehicles emit these info