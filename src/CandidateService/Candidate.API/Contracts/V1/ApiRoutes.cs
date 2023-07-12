namespace Candidate.API.Contracts.V1;

public static class ApiRoutes
{
    private const string Root = "api";
    private const string Version = "v1";

    private const string Base = $"{Root}/{Version}/candidateservice";
    
    public static class Candidates
    {
        public const string GetAll = $"{Base}/candidates";
        public const string Search = $"{Base}/candidates/search";
        public const string Create = $"{Base}/candidates";
        public const string Get = $"{Base}/candidates/{{userOfferId:guid}}";
        public const string ChangeStatus = $"{Base}/candidates/{{userOfferId:guid}}/changestatus";
    }
}