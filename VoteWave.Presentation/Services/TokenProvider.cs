using VoteWave.Presentation.Services.IServices;

namespace VoteWave.Presentation.Services
{
    public class TokenProvider(IHttpContextAccessor contextAccessor) : ITokenProvider
    {
        private const string TokenCookie = "JWTToken";
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        public void ClearToken()
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete(TokenCookie);
        }

        public string? GetToken()
        {
            string token = null;
            bool? hasToken = _contextAccessor.HttpContext.Request.Cookies.TryGetValue(TokenCookie, out token);
            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append(TokenCookie, token);
        }
    }
}
