using System.Runtime.Serialization;
using WebApplication2.Data.Models;

namespace WebApplication2.Data.Dtos
{
    [Serializable]
    internal class ExceptionDto : Exception
    {
        private Exhibition exhibition;

        public ExceptionDto()
        {
        }

        public ExceptionDto(Exhibition exhibition)
        {
            this.exhibition = exhibition;
        }

        public ExceptionDto(string? message) : base(message)
        {
        }

        public ExceptionDto(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ExceptionDto(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}