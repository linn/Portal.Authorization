namespace Linn.Portal.Authorization.Integration.Tests.AuthorizationModuleTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using FluentAssertions;

    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Integration.Tests.Extensions;
    using Linn.Portal.Authorization.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenGettingAll : ContextBase
    {
        private Privilege privilege;

        [SetUp]
        public void SetUp()
        {
            this.privilege = new Privilege("do:action", AssociationType.Retailer);

            this.PrivilegeRepository.FindAllAsync().Returns(new List<Privilege> { this.privilege });

            this.Response = this.Client.Get(
                "/portal-authorization/privileges?scopeType=Retailer",
                with =>
                    {
                        with.Accept("application/json");
                    }).Result;
        }

        [Test]
        public void ShouldReturnOk()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public void ShouldReturnJsonContentType()
        {
            this.Response.Content.Headers.ContentType.Should().NotBeNull();
            this.Response.Content.Headers.ContentType?.ToString().Should().Be("application/json");
        }

        [Test]
        public void ShouldReturnJsonBody()
        {
            var resource = this.Response.DeserializeBody<IEnumerable<PrivilegeResource>>();
            resource.First().Action.Should().Be("do:action");
        }
    }
}
