using Bogus;

namespace DotNet.Core.Simple.API.UnitTests.Commom;
public class BaseFixture
{
    public Faker Faker { get; set; }

    protected BaseFixture()
    {
        Faker = new Faker("pt_BR");
        /*.rul
            .RuleFor(x => x.Company.Name, f => f.Company.CompanyName())
            .RuleFor(x => x.Person.FirstName, f => f.Person.FirstName)
            .RuleFor(x => x.Person.LastName, f => f.Person.LastName)
            .RuleFor(x => x.Person.Email, (f, u) => $"{u.FirstName.ToLower()}.{u.LastName.ToLower()}@{f.Internet.DomainName()}")
            .RuleFor(x => x.Internet.Url, f => f.Internet.Url())
            .RuleFor(x => x.Internet.IpAddress, f => f.Internet.IpAddress())
            .RuleFor(x => x.Date.Past, (f, u) => f.Date.Past(10, DateTime.Now));
    */
    }
}
