namespace MOF.Etimad.Monafasat.SharedKernal
{
    public static class SortDirection
    {
        public const string Ascending = "ASC";
        public const string Descending = "DESC";

        public static bool IsAscending(string direction)
        {
            return (string.IsNullOrWhiteSpace(direction))
            || (string.Compare(direction, Ascending, true) == 0);
        }
    }
}
