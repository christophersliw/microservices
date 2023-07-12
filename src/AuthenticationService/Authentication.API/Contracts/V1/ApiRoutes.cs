namespace Authentication.API.Contracts.V1;

public class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";

    private const string Base = $"{Root}/{Version}/autenticationservice";
    
    public static class Identity
    {
        public const string Login = $"{Base}/identity/login";
        public const string Register = $"{Base}/identity/register";
    }
}