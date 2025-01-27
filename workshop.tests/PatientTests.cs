using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace workshop.tests;

public partial class Tests
{

    [Test]
    public async Task GetAll_PatientEndpointStatus()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { 
          
        });
        //factory.ClientOptions.BaseAddress.
        var client = factory.CreateClient( );

        // Act
        var response = await client.GetAsync("/patients");

        // Assert
        Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK);
    
    }

   
}