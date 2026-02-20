namespace CompanyEmployees.Core.Domain.Exceptions
{
    public class RefreshTokenBadRequest : Exception
    {
        public RefreshTokenBadRequest() : base("Token could not be refreshed!")
        {

        }
    }
}
