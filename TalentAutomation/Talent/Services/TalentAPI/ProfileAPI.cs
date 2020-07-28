
using Framework.Config;
using Framework.Utils;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using RestSharp;
using System;
using Talent.Models;
using Talent.Services;

namespace API.TalentAPI
{
    public class ProfileAPI
    {
        private const string UserProfileAPI = "/api/profile";
        private const string SkillAPI = "/api/profile/skills";
        public static string PostSkill(Skill skill)
        {
            ReportHelper.LogTestStepInfo($"Post a skill through API {SkillAPI}");
            var client = new RestClient(FW.Settings.Test.TalentAPI);
            var request = new RestRequest(SkillAPI)
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Authorization", $"Bearer {TokenHelper.TalentToken.TokenId}");
            request.AddJsonBody(new 
            { 
                name = skill.Name,
                level = skill.Level
            });

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"{request.Method} {SkillAPI} failed with  {response.StatusCode}");
            }

            string id = JObject.Parse(response.Content)["id"].ToString();
            return id;

        }

        public static string GetProfileId()
        {
            var client = new RestClient(FW.Settings.Test.TalentAPI);
            var request = new RestRequest(UserProfileAPI)
            {
                Method = Method.GET,
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Authorization", $"Bearer {TokenHelper.TalentToken.TokenId}");

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"{request.Method} {SkillAPI} failed with  {response.StatusCode}");
            }

            return JObject.Parse(response.Content)["profile"]["id"].ToString();
        }

        public static void DeleteSkill(string skillId)
        {
            var client = new RestClient(FW.Settings.Test.TalentAPI);
            var request = new RestRequest(SkillAPI)
            {
                Method = Method.DELETE,
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Authorization", $"Bearer {TokenHelper.TalentToken.TokenId}");
            request.AddQueryParameter("id", skillId);

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"{request.Method} {SkillAPI} failed with  {response.StatusCode}");
            }
        }

        public static void ResetProfile()
        {
            var client = new RestClient(FW.Settings.Test.TalentAPI);
            var request = new RestRequest(UserProfileAPI)
            {
                Method = Method.POST,
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Authorization", $"Bearer {TokenHelper.TalentToken.TokenId}");
            string profileId = GetProfileId();
            //todo: optimize the json body
            request.AddParameter("application/json", "   {\r\n   \t\t\"id\":\"" + profileId + "\",\r\n        \"firstName\": \"talent\",\r\n        \"middleName\": null,\r\n        \"lastName\": \"MVP\",\r\n        \"username\": \"talent\",\r\n        \"gender\": null,\r\n        \"email\": \"talent.ivy@mvp.studio\",\r\n        \"phone\": null,\r\n        \"mobilePhone\": null,\r\n        \"isMobilePhoneVerified\": true,\r\n        \"address\": {\r\n            \"number\": \"\",\r\n            \"street\": \"\",\r\n            \"suburb\": \"\",\r\n            \"postCode\": \"0\",\r\n            \"city\": \"\",\r\n            \"country\": \"\"\r\n        },\r\n        \"nationality\": null,\r\n        \"visaStatus\": null,\r\n        \"visaExpiryDate\": null,\r\n        \"profilePhoto\": \"6c754d80-8605-4ff8-ac70-849f1692aedaCapture.PNG\",\r\n        \"profilePhotoUrl\": \"http://talent-service.user-profile.s3.amazonaws.com/6c754d80-8605-4ff8-ac70-849f1692aedaCapture.PNG\",\r\n        \"videoName\": \"a80c459e-2b70-42ca-a70a-907e6bd0737bdemo.webm\",\r\n        \"videoUrl\": \"https://talent-service-video.s3.ap-southeast-2.amazonaws.com/a80c459e-2b70-42ca-a70a-907e6bd0737bdemo.webm?X-Amz-Expires=86400&x-amz-security-token=FwoGZXIvYXdzEDAaDP66cXVnKcNE7eUjzSKBAdNimzidnCR%2BdM%2FzVaG0gPFxcwhRFsB6RYTbaxGFzpru%2FM%2FqYKefbqqOzgdF7kqTGsjhDrdlVufEW3RGsp%2Fgp5VOJxfELysfk3HOQwI4NJYm2buyEqqH6B2m3lDvDK1SJOnE3Je7zHH%2BAowR7y%2Bu527sXrRDaMkAt9erBK%2Fv85%2BPUSit7PH1BTIo6jjsrQ3RSvnMK1gWXilwe9C1vxkrX7gq5idu%2BXgQLH25fay9xq%2BKyg%3D%3D&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAXHJYYX32IDJOO7P3/20200513/ap-southeast-2/s3/aws4_request&X-Amz-Date=20200513T223525Z&X-Amz-SignedHeaders=host;x-amz-security-token&X-Amz-Signature=52d5fc81f46bbfd59f69b2cfb623600a2f60addaca8944ea5edd97375323e19e\",\r\n        \"cvName\": \"03493a97-e04b-4a1a-95d4-e6fd334f807fCV.docx\",\r\n        \"cvUrl\": \"https://s3.ap-southeast-2.amazonaws.com/talent-service.user-profile/03493a97-e04b-4a1a-95d4-e6fd334f807fCV.docx?X-Amz-Expires=86400&x-amz-security-token=FwoGZXIvYXdzEDAaDNeK5yM6RTrYxUQgByKBAbw1gtfD6vS3ob3uTXmSDNaIeZC8vNhEqRsC%2F7IR%2FGi2Q13os8CiNFdjWNMGOCsw%2FjsFWSZbUA5%2B%2B3W8XWkLOuSt035bgrky40hKrWmBhsDpvYiFuPBqLn%2B2PvMEHssik2%2BP0RfAXbGBnpykWfQqTSkAHCUKx%2B9IiDgk%2BIEH3p8a3Siu7PH1BTIoJv37JFyqXbvc6WdjMqJU6lXQLGv9Taq%2B%2FtK3fAIXCuPj3Em3vDZdsw%3D%3D&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAXHJYYX32GVQ4TOM2/20200513/ap-southeast-2/s3/aws4_request&X-Amz-Date=20200513T223526Z&X-Amz-SignedHeaders=host;x-amz-security-token&X-Amz-Signature=6efdf2148d594fbd767a0fe22dc9b4c578ac9ea6fae0e08d411cb1b52d8df40f\",\r\n        \"summary\": null,\r\n        \"description\": null,\r\n        \"linkedAccounts\": [],\r\n        \"jobSeekingStatus\": {\r\n            \"status\": \"\",\r\n            \"availableDate\": null\r\n        },\r\n        \"languages\": [],\r\n        \"skills\": [],\r\n        \"educations\": [],\r\n        \"certifications\": [],\r\n        \"experiences\": []\r\n    }", ParameterType.RequestBody);

            var response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"{request.Method} {UserProfileAPI} failed with  {response.StatusCode}");
            }

        }
    }
}