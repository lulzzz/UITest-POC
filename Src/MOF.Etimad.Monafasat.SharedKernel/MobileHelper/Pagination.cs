using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.SharedKernel.MobileHelper
{
    public class Pagination
    {
        public Pagination()
        {

        }
        public int pageCount { get; set; }
        public int itemCountPerPage { get; set; }
        public int first { get; set; }
        public int current { get; set; }
        public int last { get; set; }
        public int previous { get; set; }
        public int next { get; set; }
        public Dictionary<string, int> pagesInRange { get; set; }
            = new Dictionary<string, int>();
        public int firstPageInRange { get; set; }
        public int lastPageInRange { get; set; }
        public int currentItemCount { get; set; }
        public int totalItemCount { get; set; }
        public int firstItemNumber { get; set; }
        public int lastItemNumber { get; set; }

    }
}
