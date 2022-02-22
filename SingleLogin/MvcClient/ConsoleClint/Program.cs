// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;

Console.WriteLine("Hello, World!");
// discovery endpoint
var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5200/");

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
    Scope = "api1"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

// call Identity Resource API
var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync(disco.UserInfoEndpoint);

//var response = await apiClient.GetAsync("http://localhost:5001/identity");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    //Console.WriteLine(JArray.Parse(content));
}

Console.ReadKey();