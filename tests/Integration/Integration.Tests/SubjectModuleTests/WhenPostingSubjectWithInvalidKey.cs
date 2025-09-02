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

    public class WhenPostingSubjectWithInvalidKey : ContextBase
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

            request.Headers.Add("X-Shared-Secret", "a-wrong-guess");

            this.Response = this.Client.SendAsync(request).Result;
        }

        [Test]
        public void ShouldReturnUnauthorized()
        {
            this.Response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public void ShouldNotAdd()
        {
            this.Repository.DidNotReceive()
                .AddSubject(Arg.Any<Subject>());
        }

        [Test]
        public void ShouldNotCommit()
        {
            this.TransactionManager.DidNotReceive().CommitAsync();
        }
    }
}
