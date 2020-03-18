using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebAPI.Models
{
    public enum VehicleType
    {
        Truck,
        Van
    }


    public class Vehicle : BaseEntity
    {
        internal string _connectionDetails { get; set; }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public VehicleType Type { get; set; }

        [MaxLength(50)]
        [Required]
        public string PlateNumber { get; set; }

        [Column(name: "CurrentSpeed")]
        public float Speed { get; set; } //in kmph

        [Column(name: "CurrentLatitude")]
        public double Latitude { get; set; }

        [Column(name: "CurrentLongitude")]
        public double Longitude { get; set; }

        [Column(name:"CurrentEngineTemperature")]
        public float EngineTemperture { get; set; } //in celcius

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }

        [NotMapped]
        public JObject ConnectionDetails
        {
            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(_connectionDetails) ? "{}" : _connectionDetails);
            }
            set
            {
                _connectionDetails = JsonConvert.SerializeObject(value);
            }
        }

    }
}
