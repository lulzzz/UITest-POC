namespace MOF.Etimad.Monafasat.Integration
{
    public class PayorPOIServiceModel
    {
        public PayorPOIServiceModel()
        {

        }
        public PayorPOIServiceModel(string poiNumber, string poiType)
        {
            POINumber = poiNumber;
            POIType = poiType;
        }
        public string POINumber { get; set; }
        public string POIType { get; set; }
    }
}
