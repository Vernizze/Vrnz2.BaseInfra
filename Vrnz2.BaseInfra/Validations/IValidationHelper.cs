using Vrnz2.BaseContracts.DTOs.Base;
using System.Collections.Generic;

namespace Vrnz2.BaseInfra.Validations
{
    public interface IValidationHelper
    {
        (bool IsValid, List<string> ErrorCodes) Validate<T>(T request, bool ignoreMessageCodesFactory = false)
            where T : BaseDTO.Request;
    }
}
