// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

Console.WriteLine("Hello, World!");

// discovery endpoint
var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5900/");

if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

// request access token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "console client", 
    ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
    GrantType = "client_credentials",
    Scope = "api1",// openid profile offlineAccess
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

// call Identity Resource API
var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);
var response = await apiClient.GetAsync("http://localhost:5901/identity");

//var response = await apiClient.GetAsync(disco.UserInfoEndpoint);
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(JArray.Parse(content));
}

Console.ReadKey();