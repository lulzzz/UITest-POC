namespace MOF.Etimad.Monafasat.Integration
{
    public class IntegrationCityModel
    {
        public IntegrationCityModel()
        {
        }
        public IntegrationCityModel(string cityName, string cityCode)
        {
            CityName = cityName;
            CityCode = cityCode;
        }
        public string CityName { get; set; }
        public string CityCode { get; set; }
    }
}
