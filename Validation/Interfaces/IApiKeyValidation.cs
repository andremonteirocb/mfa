﻿namespace MFA.Validation.Interfaces
{
    public interface IApiKeyValidation
    {
        bool IsValidApiKey(string userApiKey);
    }
}
