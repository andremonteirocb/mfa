using Google.Authenticator;
using Microsoft.AspNetCore.Mvc;

namespace MFA
{
    public static class Endpoints
    {
        public static void AddEndpointsAuthenticator(this WebApplication app)
        {
            app.MapGet("/generateqr", ([FromServices] TwoFactorAuthenticator _tfa, string accountTitleNoSpaces, string accountSecretKey) =>
            {
                SetupCode setupInfo = _tfa.GenerateSetupCode("Phidelis", accountTitleNoSpaces, accountSecretKey, false, 3);
                return setupInfo.QrCodeSetupImageUrl;
            })
            .WithName("generateqr")
            .Produces<string>()
            .WithOpenApi();

            app.MapGet("/validatecode", ([FromServices] TwoFactorAuthenticator _tfa, string accountSecretKey, string twoFactorCodeFromClient) =>
            {
                return _tfa.ValidateTwoFactorPIN(accountSecretKey, twoFactorCodeFromClient);
            })
            .WithName("validatecode")
            .Produces<bool>()
            .WithOpenApi();
        }
    }
}
