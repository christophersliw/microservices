using Candidate.Domain.Entities;
using Candidate.Fixtures;
using Candidate.Persistence.EF.Repositories;
using Xunit;

namespace Candidate.Repository.Tests;

public class UserRepositoryTests : IClassFixture<CandidateDbContextFactory>
{
    private readonly CandidateDbContextFactory _factory;

    public UserRepositoryTests(CandidateDbContextFactory factory)
    {
        _factory = factory;
    }
    
    [Theory]
    [MemberData(nameof(UserTestData))]
    public async Task GetByIdAsync_ShouldReturnUser(User user)
    {
        var sut = new UserRepository(_factory.CandidateDbContextTestInstance);

        var result = await sut.GetByIdAsync(user.Id);

        Assert.Equal(user.Id, result.Id);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnUsers()
    {
        var sut = new UserRepository(_factory.CandidateDbContextTestInstance);

        var result = await sut.GetAllAsync();

        Assert.Equal(1, result.Count);
    }

    public static TheoryData<User> UserTestData()
    {
        return new()
        {
            {new User(){Id = new Guid("B30461FE-F05E-4B65-B4C7-7FA1B45C6B0A"), Surrname = "Kowalski", FirstName = "Jan"}}
        };
    }
}