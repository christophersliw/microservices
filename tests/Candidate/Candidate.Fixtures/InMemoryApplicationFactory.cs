using Candidate.Persistence.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Candidate.Fixtures;

public class InMemoryApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("TestCandidate").ConfigureTestServices(services =>
        {
            var contextOptions = new
                    DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            services.AddScoped<CandidateDbContext>(provider => new CandidateDbContextTest(contextOptions));
            
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CandidateDbContext>();
                db.Database.EnsureCreated();
            }

        });
    }
}