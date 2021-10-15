using System;
using System.Runtime.Serialization;

namespace Api.App.Exceptions
{
    [Serializable]
    public class ConnectMySqlException : Exception
    {
        const string MESSAGE = "Błąd połączenia z bazą danych";
        public ConnectMySqlException() : base(MESSAGE)
        {
        }

        public ConnectMySqlException(string message, Exception innerException) : base(MESSAGE, innerException)
        {
        }

        protected ConnectMySqlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}