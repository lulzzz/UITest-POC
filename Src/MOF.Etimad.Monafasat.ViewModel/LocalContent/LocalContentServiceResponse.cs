namespace MOF.Etimad.Monafasat.ViewModel
{
    public class LocalContentServiceResponse<T> 
    {
        public T content { get; set; }
        public string msg { get; set; }
        public bool isSuccess { get; set; }
    }
}