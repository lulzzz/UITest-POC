using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BiddingDateTimeViewModel
    {
        public string TenderIdString { get; set; }

        [JsonConverter(typeof(CustomDateTimeFormatForJsonSerlized))]
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BiddingDate { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public DateTime BiddingStartDateTime { get; set; }
        public DateTime BiddingEndDateTime { get; set; }
    }
}
