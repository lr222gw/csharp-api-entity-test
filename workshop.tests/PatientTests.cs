using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace workshop.tests;

public partial class Tests
{

    public class Patient
    {
        [Test]
        public async Task GetAll_PatientEndpointStatus()
        {
            // Arrange
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => {

            });
            //factory.ClientOptions.BaseAddress.
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("patients");

            // Assert
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK);

        }

        [TestCase(1, HttpStatusCode.OK)]
        [TestCase(321, HttpStatusCode.NotFound)]
        public async Task Get_PatientEndpointStatus(int doctorId, HttpStatusCode expected)
        {
            // Arrange
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync($"patients/{doctorId}");


            // Assert
            Assert.That(response.StatusCode == expected);
        }

    }


}