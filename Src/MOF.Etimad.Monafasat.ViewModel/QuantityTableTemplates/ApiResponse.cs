﻿namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ApiResponse<T> where T : class
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}