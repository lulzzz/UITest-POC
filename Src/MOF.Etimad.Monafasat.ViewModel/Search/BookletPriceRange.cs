namespace MOF.Etimad.Monafasat
{
    public class BookletPriceRange
    {

        public BookletPriceRange() { }
        public BookletPriceRange(string range)
        {
            switch (range)
            {
                case "0":
                    start = 0;
                    end = 0;
                    break;
                case "1":
                    start = 1;
                    end = 1000;
                    break;
                case "2":
                    start = 1001;
                    end = 10000;
                    break;
                case "3":
                    start = 10001;
                    end = 20000;
                    break;
                case "4":
                    start = 20001;
                    end = 40000;
                    break;
                case "5":
                    start = 40001;
                    end = 50000;
                    break;
                case "6":
                    start = 50001;
                    end = 1000000000;
                    break;
            }
        }

        public int start
        {
            set; get;
        }
        public int end { set; get; }


    }
}
