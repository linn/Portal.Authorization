namespace Linn.Portal.Authorization.Integration.Tests.SubjectModuleTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;

    using FluentAssertions;

    using Linn.Common.Configuration;
    using Linn.Portal.Authorization.Domain;
    using Linn.Portal.Authorization.Resources;

    using NSubstitute;

    using NUnit.Framework;

    public class WhenPostingSubject : ContextBase
    {
        private SubjectResource resource;

        private string guidString;

        [SetUp]
        public void SetUp()
        {
            this.guidString = Guid.NewGuid().ToString();
            this.resource = new SubjectResource { Sub = this.guidString };

            var request = new HttpRequestMessage(HttpMethod.Post, "/portal-authorization/subjects")
                              {
                                  Content = JsonContent.Create(this.resource)
                              };
            ConfigurationManager.Configuration["AUTHORIZATION_API_KEY"] = "top-secret-string";
            request.Headers.Add("X-Shared-Secret", "top-secret-string");

            this.Response = this.Client.SendAsync(request).Result;
        }

        [Test]
        public void ShouldReturnCreated()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public void ShouldAdd()
        {
            this.Repository.Received(1)
                .AddSubject(Arg.Is<Subject>(s => s.Sub.ToString() == this.guidString));
        }

        [Test]
        public void ShouldCommit()
        {
            this.TransactionManager.Received(1).CommitAsync();
        }
    }
}
