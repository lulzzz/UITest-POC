namespace MOF.Etimad.Monafasat.Integration
{
    public class CROwner
    {
        public CROwner()
        {
        }
        public CROwner(string name, string nationalityEn, string nationalityAr, string relation)
        {
            Name = name;
            NationalityEn = nationalityEn;
            NationalityAr = nationalityAr;
            Relation = relation;
        }
        public string Name { get; set; }
        public string NationalityEn { get; set; }
        public string NationalityAr { get; set; }
        public string Relation { get; set; }
    }
}
