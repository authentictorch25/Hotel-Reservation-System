using System;
using System.Collections.Generic;
using System.Text;

namespace HotelResevationSystem
{
    class HotelReservationCustomException: Exception
    {
        public enum ExceptionType
        {
            INVALID_DATE_FORMAT,
            INVALID_DATE_RANGE,
            INVALID_CUSTOMER_TYPE
        }
        public ExceptionType exceptionType;
        /// <summary>
        /// Initializes a new instance of the <see cref="HotelReservationCustomException"/> class.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <param name="message">The message.</param>
        public HotelReservationCustomException(ExceptionType exceptionType, string message) : base(message)
        {
            this.exceptionType = exceptionType;
        }
    }
}
