using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CheckingDateTimeViewModel
    {
        public string TenderIdString { get; set; }
        [JsonConverter(typeof(CustomDateTimeFormatForJsonSerlized))]
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]

        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterOffersCheckingData")]
        public DateTime CheckingDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterOffersCheckingTime")]
        public string CheckingTime { get; set; }
        public DateTime CheckingDateTime { get { return CheckingDate + GetTimePart(this.CheckingTime); } set { } }
        public string Notes { get; set; }

        public static TimeSpan GetTimePart(string time)
        {
            TimeSpan ts = new TimeSpan();
            if (!string.IsNullOrEmpty(time))
            {
                ts = Convert.ToDateTime(time).TimeOfDay;
            }
            return ts;
        }
    }
}
