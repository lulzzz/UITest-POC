using System.Runtime.Serialization;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ApiResponseObject<T>
    {
        private T _data;
        private bool _success = true;
        private int _total;
        private string _message;

        [DataMember()]
        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        [DataMember()]
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }

        [DataMember()]
        public T data
        {
            get { return _data; }
            set { _data = value; }
        }

        public ApiResponseObject(T data)
        {
            _data = data;
        }
        public ApiResponseObject(string message, bool isSuccess)
        {
            _message = message;
            _success = isSuccess;
        }
    }
}
