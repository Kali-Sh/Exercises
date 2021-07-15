using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AnimalSimulation.Exceptions
{
    public class PositionOutOfRangeException : Exception
    {
        private const string Invalid_position_exception_message = "Invalid position";

        public PositionOutOfRangeException()
            : base(Invalid_position_exception_message)
        {

        }

        public PositionOutOfRangeException(string message) : base(message)
        {
        }

        public PositionOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PositionOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
