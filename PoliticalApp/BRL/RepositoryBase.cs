using Common.CommonUtil.Constants;
using Common.CommonUtil.Exceptions;
using CommonUtilily.Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.PoliticalAppRepository
{
    public abstract class RepositoryBase
    {
        #region Public Methods

        protected internal void HandleServerException(Exception exception)
        {
            //Log exception in file/db before throwing back
            //We can handle different server exceptions here by type and can take the custom actions
            if (exception is DbEntityValidationException)
            {
                var errorMessages = ((DbEntityValidationException)exception).EntityValidationErrors
                                    .SelectMany(x => x.ValidationErrors)
                                    .Select(x => x.ErrorMessage);
                string fullErrorMessage = string.Join("; ", errorMessages);
                throw new CustomException(fullErrorMessage, ErrorCodes.ValidationError, exception);
            }
            else
            {
                throw new CustomException(exception.Message, exception);
            }
        }

        protected internal void HandleNullException(object result)
        {
            if (result == null)
            {
                throw new CustomException(CommonConstants.NoRecordFoundMessage, ErrorCodes.NoRecordFoundError);
            }
        }

        protected internal void HandleNullException(ICollection result)
        {
            if (result == null || result.Count == 0)
            {
                throw new CustomException(CommonConstants.NoRecordFoundMessage, ErrorCodes.NoRecordFoundError);
            }
        }
        #endregion
    }
}
