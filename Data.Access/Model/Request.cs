using Newtonsoft.Json;
using System;
//using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Access.Model
{
    [Table("request")]
    public class Request
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("ix")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[System.ComponentModel.DataAnnotations.Key/*, DatabaseGenerated(DatabaseGeneratedOption.None), JsonProperty("ix")*/]
        public int Index { get; set; }
        public string Name { get; set; }
        public int? Visits { get; set; }
        public DateTime Date { get; set; }
    }
}
