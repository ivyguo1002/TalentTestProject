using Framework.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using Talent.Models;

namespace GTIO.Framework.Services.IdentityAPI
{
    public class IdentityAPI
    {
        private const string SignInAPI = "/api/auth/signin";
        public static Token GetToken(string userEmail, string userPassword)
        {
            var client = new RestClient(FW.Settings.Test.IdentityAPI);
            var request = new RestRequest(SignInAPI)
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new
            {
                email = userEmail,
                password = userPassword
            });

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"{request.Method} {SignInAPI} failed with  {response.StatusCode}");
            }

            var tokenString = JObject.Parse(response.Content)["token"].ToString();
            return JsonConvert.DeserializeObject<Token>(tokenString);   
        }
    }
}