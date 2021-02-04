using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Program
{

    private const string _clientId = "fb63b2d2-03ac-49b7-8912-cbd747fc3abb";
    private const string _tenantId = "40dfab26-92e7-4489-a461-238c6ddd03f6";

    public static async Task Main(string[] args)
    {
        IPublicClientApplication app;

        app = PublicClientApplicationBuilder
            .Create(_clientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
            .WithRedirectUri("http://localhost")
            .Build();

        List<string> scopes = new List<string> 
        { 
            "user.read" 
        };

        AuthenticationResult result;
/*
        result = await app
            .AcquireTokenInteractive(scopes)
            .ExecuteAsync();

        Console.WriteLine($"Token:\t{result.AccessToken}");
*/
        DeviceCodeProvider provider = new DeviceCodeProvider(app, scopes);

        GraphServiceClient client = new GraphServiceClient(provider);

        User myProfile = await client.Me
            .Request()
            .GetAsync();

        Console.WriteLine($"Name:\t{myProfile.DisplayName}");
        Console.WriteLine($"AAD Id:\t{myProfile.Id}");   
    }
}