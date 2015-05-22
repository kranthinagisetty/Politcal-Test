using Common.CommonUtil.Constants;
using CommonUtilily.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommonUtil.Exceptions
{
    public class CustomException : Exception
    {
        #region Public Properties
        public int ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
        #endregion

        #region Public Constructors
        public CustomException(string errorMessage, ErrorCodes errorCode)
        {
            SetupCustomException(errorCode, errorMessage);
        }

        public CustomException(string errorMessage, Exception exception)
            : base(errorMessage, exception)
        {
            SetupCustomException(ErrorCodes.GenericError, errorMessage);
        }

        public CustomException(string errorMessage, ErrorCodes errorCode, Exception exception)
            : base(errorMessage, exception)
        {
            SetupCustomException(errorCode, errorMessage);
        }
        #endregion

        #region Private Methods
        private void SetupCustomException(ErrorCodes errorCode, string errorMessage = CommonConstants.GenericErrorMessage)
        {
            ErrorCode = (int)errorCode;
            ErrorMessage = errorMessage;
        }
        #endregion
    }
}
