using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Movies.Client.HttpHandlers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
    
    // private readonly IHttpClientFactory _httpClientFactory;
    // private readonly ClientCredentialsTokenRequest _tokenRequest;
    //
    // public AuthenticationDelegatingHandler(
    //     IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest tokenRequest)
    // {
    //     _httpClientFactory = httpClientFactory;
    //     _tokenRequest = tokenRequest;
    // }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // var httpClient = _httpClientFactory.CreateClient("IDPClient");
        //
        // var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_tokenRequest, cancellationToken);
        //
        // if (tokenResponse.IsError)
        //     throw new HttpRequestException("Something went wrong while requesting the access token");
        //
        // request.SetBearerToken(tokenResponse.AccessToken);

        if (_httpContextAccessor.HttpContext == null)
            throw new NullReferenceException("Не найден объект HttpContext!");

        var accessToken = await _httpContextAccessor.HttpContext
            .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        
        if (!string.IsNullOrWhiteSpace(accessToken))
            request.SetBearerToken(accessToken);
        
        return await base.SendAsync(request, cancellationToken);
    }
}