using FluentValidation;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.BaseInfra.MessageCodes;
using Vrnz2.Infra.CrossCutting.Extensions;

namespace Vrnz2.BaseInfra.Validations
{
    public class ValidationHelper
    {
        #region Variables

        private readonly ILogger _logger;
        private readonly IValidatorFactory _validatorFactory;

        #endregion 

        #region Constructor

        public ValidationHelper(ILogger logger, IValidatorFactory validatorFactory)
        {
            _logger = logger;
            _validatorFactory = validatorFactory;
        }

        #endregion 

        #region Methods

        public (bool IsValid, List<string> ErrorCodes) Validate<T>(T request, bool ignoreMessageCodesFactory = false)
            where T : BaseDTO.Request
        {
            try
            {
                var validator = _validatorFactory.GetValidator(request.GetType());

                if (validator.IsNull())
                    return (false, new List<string> { string.Empty });

                var context = new ValidationContext<T>(request);

                var validationResult = validator.Validate(context);

                if (ignoreMessageCodesFactory)
                    return (validationResult.IsValid, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
                else
                    return (validationResult.IsValid, validationResult.Errors.Select(e => MessageCodesFactory.Instance.GetMessage(e.ErrorMessage)).ToList());
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Unexpected error! - Message: {ex.Message}", ex);

                return (false, new List<string> { MessageCodesFactory.UNEXPECTED_ERROR });
            }
        }

        #endregion 
    }
}
