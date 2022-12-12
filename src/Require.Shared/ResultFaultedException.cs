using System.Runtime.Serialization;

namespace Require.Shared
{
    [Serializable]
    internal class ResultFaultedException : Exception
    {
        public ResultFaultedException()
        {
        }

        public ResultFaultedException(string? message) : base(message)
        {
        }

        public ResultFaultedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ResultFaultedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}