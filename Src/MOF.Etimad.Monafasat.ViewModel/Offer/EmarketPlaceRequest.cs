namespace MOF.Etimad.Monafasat.ViewModel
{
    public class EmarketPlaceRequest
    {
        public int FormId { get; set; }
        public long TableId { get; set; }
    }

    public class EmarketPlaceResponse
    {

        public long FormId { get; set; }
        public long ColumnId { get; set; }
        public int ColumnTypeId { get; set; }


    }

}
