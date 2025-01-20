using Cmd.Application.Tools.Email;
using Cms.Domain.Models.Token.Entities;
using Cms.Domain.Models.Token.Repositories;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cmd.Application.Tools.Sms.Model
{
    public class Sms
    {
        private readonly SmsOp _appSettings;
        private readonly HttpClient _httpClient;
        private readonly ITokenRepository _tokenRepository;

        public Sms(IOptions<SmsOp> option, HttpClient httpClient, ITokenRepository tokenRepository)
        {
            _appSettings = option.Value;
            _httpClient = httpClient;
            _tokenRepository = tokenRepository;
        }


        public async Task SendSmsAsync(string phoneNumber, string text)
        {
            //Message msg = new Message();
            //msg.Sender = _appSettings.SenderLine;
            //msg.Receptor = phoneNumber;
            //msg.Text = text;

            //var client = new RestClient(_appSettings.ApimUrl);
            //var requestBody = new RestRequest(_appSettings.TokenAddress, Method.Post);

            //requestBody.AddHeader("Content-Type", "application/json");

            //requestBody.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials", ParameterType.RequestBody);

            //var authenticationString = $"{_appSettings.CustomerKey}:{_appSettings.SecretKey}";
            //var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));
            //requestBody.AddHeader("Authorization", $"Basic {base64EncodedAuthenticationString}");

            //var response = client.Execute(requestBody);                                 
            //var token = JsonConvert.DeserializeObject<APIMToken>(response.Content);

            //var smsClient = new RestClient(_appSettings.SmsServiceUrl);

            //var smsRequestBody = new RestRequest($"/{_appSettings.ApiKey}/sms/send.json?sender={_appSettings.SenderLine}&receptor={phoneNumber}&message={text}", Method.Get);
            //smsRequestBody.AddHeader("Authorization", $"Bearer {token.AccessToken}");
            //client.Execute(smsRequestBody);

            APIMToken token;

            HttpResponseMessage response;
            String responseBody;

            //if (_tokenRepository.ApiTokenAvailable())
            //{
            //    var tokenModel = _tokenRepository.GetApiToken();
            //    token = new APIMToken
            //    {
            //        access_token = tokenModel.access_token,
            //        expires_in = tokenModel.expires_in,
            //        scope = tokenModel.scope,
            //        token_type = tokenModel.token_type
            //    };
            //}
            //else
            //{
            //    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.isfahan.ir/apim/oauth2/token");

            //    var authenticationString = $"1t1zQKfAgVCbgyGPejlo8Bh3LZIa:ffWu75f4oZAOwHzoq2sDrxGzTFMa";
            //    var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));

            //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

            //    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    var content = new StringContent($"grant_type=client_credentials");
            //    content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            //    request.Content = content;

            //    response = await _httpClient.SendAsync(request);
            //    responseBody = await response.Content.ReadAsStringAsync();
            //    token = JsonConvert.DeserializeObject<APIMToken>(responseBody);

            //    _tokenRepository.AddApiToken(token.access_token,token.scope,token.token_type,token.expires_in);
            //}

            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            //var smsRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.isfahan.ir/apim/services/t/fava.isf/kavenegar/1.0/v1/47657152613466776F353273357145637249456F3665475964446B75756345527449466A644879592F6F4D3D/sms/send.json?sender=10006000330033&receptor={phoneNumber}&message={text}");

            //response = await _httpClient.SendAsync(smsRequest);
            //responseBody = await response.Content.ReadAsStringAsync();
        }
    }
}
