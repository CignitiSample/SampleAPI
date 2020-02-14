using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using RelevantCodes.ExtentReports;

namespace MCS.Test.Automation.Common.Helpers
{
    public static class APIResponse
    {

        public static async Task<List<HttpResponseMessage>> GetResponse(string apiBaseAddress, string jsonData)
        {
            try
            {
                List<HttpResponseMessage> listOfResponse = new List<HttpResponseMessage>();

                CustomDelegatingHandler customDelegatingHandler = new CustomDelegatingHandler();
                HttpClient client = HttpClientFactory.Create(customDelegatingHandler);
                List<string> inputJsonData = ReadJsonFile(jsonData);
                foreach (string data in inputJsonData)
                {
                    // jsonData = ValidateAddressApiPayload(); //This can be deleted once pass the data from common- SAI              

                    DriverContext.ExtentStepTest.Log(LogStatus.Info, "Sending request for API : " + apiBaseAddress, "Request Data : " + data);
                    DateTime startTime = DateTime.Now;
                    // Do some work   
                    HttpResponseMessage response = null;
                    if (string.IsNullOrEmpty(data))
                    {
                        response = await client.GetAsync(apiBaseAddress);
                    }
                    else
                    {
                        response = await client.PostAsync(apiBaseAddress, new StringContent(data, Encoding.UTF8, "application/json"));
                    }
                    //return response;
                    string responseString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {

                        //DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Received response for API :  " + apiBaseAddress, "HTTP Status: " + response.StatusCode + "  Reason  " + response.ReasonPhrase);
                        DriverContext.ExtentStepTest.Log(LogStatus.Pass, "Received response ", "Response: " + responseString + "\n" + "HTTP Status: " + response.StatusCode + " Reason :" + response.ReasonPhrase);
                    }
                    else
                    {
                        DriverContext.ExtentStepTest.Log(LogStatus.Fail, "Received response as Payload validation failed, Please try to reprocess with correct information", "Response: " + responseString + "\n" + "HTTP Status: " + response.StatusCode + " Reason :" + response.ReasonPhrase);
                    }
                    TimeSpan timeDiff = DateTime.Now - startTime;
                    DriverContext.ExtentStepTest.Log(LogStatus.Info, "API Response time", Convert.ToString(timeDiff.TotalMilliseconds) + " ms");
                    listOfResponse.Add(response);
                }
                //Console.ReadLine();
                return listOfResponse;
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Info, "GetResponse : Failed due to  error " + ex.Message);
                return null;
            }
        }

        public class CustomDelegatingHandler : DelegatingHandler
        {
            //Obtained from the server earlier, APIKey MUST be stored securly and in App.Config
            private string APPId = "4d53bce03ec34c0a911182d4c228ee6c";
            private string APIKey = "A93reRTUJHsCuQSHR+L3GxqOJyDmQpCgps102ciuabc=";


            //private string APPId = "16a8fade6ecf44e0a4b3eb3fffec8e80";
            //private string APIKey = "b09324f3db334d32b47d3fedf49856be";
            // azure auth token
            private string azureAuthKey = GetAzureToken();

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {

                HttpResponseMessage response = null;
                string requestContentBase64String = string.Empty;

                string requestUri = System.Web.HttpUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToLower());

                string requestHttpMethod = request.Method.Method;

                //Calculate UNIX time
                DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan timeSpan = DateTime.UtcNow - epochStart;
                string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

                //create random nonce for each request
                string nonce = Guid.NewGuid().ToString("N");

                //Checking if the request contains body, usually will be null wiht HTTP GET and DELETE
                if (request.Content != null)
                {
                    //request.Content.ReadAsStringAsync().Result
                    string contentstr = request.Content.ReadAsStringAsync().Result;
                    byte[] content = Encoding.Unicode.GetBytes(contentstr);
                    MD5 md5 = MD5.Create();
                    //Hashing the request body, any change in request body will result in different hash, we'll incure message integrity
                    byte[] requestContentHash = md5.ComputeHash(content);
                    requestContentBase64String = Convert.ToBase64String(requestContentHash);
                }

                //Creating the raw signature string
                string signatureRawData = String.Format("{0}{1}{2}{3}{4}{5}", APPId, requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);

                var secretKeyByteArray = Convert.FromBase64String(APIKey);

                byte[] signature = Encoding.UTF8.GetBytes(signatureRawData);
                string azureAuthKey = GetAzureToken();
                using (HMACSHA256 hmac = new HMACSHA256(secretKeyByteArray))
                {
                    byte[] signatureBytes = hmac.ComputeHash(signature);
                    string requestSignatureBase64String = Convert.ToBase64String(signatureBytes);
                    //Setting the values in the Authorization header using custom scheme (amx)
                    string apikey = string.Format("{0}:{1}:{2}:{3}", APPId, requestSignatureBase64String, nonce, requestTimeStamp);
                    request.Headers.Add("ApiKey", string.Format("{0}:{1}:{2}:{3}", APPId, requestSignatureBase64String, nonce, requestTimeStamp));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", azureAuthKey);
                }

                response = await base.SendAsync(request, cancellationToken);
                return response;
            }
        }


        public static string Test()
        {
            string contentToHash = "Test webapi content!";
            byte[] requestBlob = Encoding.Unicode.GetBytes(contentToHash);
            MD5 md5obj = MD5.Create();
            //Hashing the request body, any change in request body will result in different hash, we'll incure message integrity
            byte[] hash = md5obj.ComputeHash(requestBlob);
            string hashContentBase64String = Convert.ToBase64String(hash);

            string contentString = "Test webapi content!";
            string keyString = "b09324f3db334d32b47d3fedf49856be";
            byte[] contentBlob = Encoding.UTF8.GetBytes(contentString);
            byte[] privateKeyBlob = Convert.FromBase64String(keyString);
            byte[] hmacBlob;
            using (HMACSHA256 hmac = new HMACSHA256(privateKeyBlob))
            {
                hmacBlob = hmac.ComputeHash(contentBlob);
            }
            string hmacBlobBase64String = Convert.ToBase64String(hmacBlob);

            //string privateKeyHex = Program.ByteArrayToHexString(privateKeyBlob);
            //string hmacOutHex = Program.ByteArrayToHexString(hmacBlob);

            return string.Empty;
        }
        public static byte[] FromHex(string hex)
        {
            var result = new byte[hex.Length / 2];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return result;
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private static string AddOrderApiPayload()
        {
            var addOrderObj = new JavaScriptSerializer().Deserialize<dynamic>(
                        @"{
    'orderdate': '2019/12/05',
    'promominor': '',
    'creditcard': '6677788',
    'ordertype': 'REGULAR',
    'webstore': 'PTP',
    'accountnumber': '',
    'shippingaccountnumber': '',
    'shippingmethod': 'Standard Service',
    'accountpartytype': 'Organization',
    'lineitems': [
        {
            'linenumber': 1,
            'orderquantity': '1',
            'stock': 'PTPALUM2019',
            'price': '259.00'
        }
    ],
    'mcxordernumber': 'PTPPOSP09120519001',
    'promomajor': '',
    'shippingamount': '0.00',
    'sites': [
        {
            'siteid': '',
            'sitetype': 'billto',
            'address': {
                'postalcode': '15469',
                'city': 'NORMALVILLE',
                'streetaddress1': 'NORMALVILLE',
                'country': 'US',
                'stateorprovince': 'PA'
            },
            'contact': {
                'contactno': '6786786789',
                'firstname': 'Prabhakar',
                'lastname': 'Mishra',
                'email': 'prabhakar.mishra@test.org',
                'organizationorcompany': 'Icreon'
            }
        },
        {
            'contact': {
                'email': 'prabhakar.mishra@test.org',
                'organizationorcompany': 'Icreon',
                'lastname': 'Mishra',
                'firstname': 'Prabhakar',
                'contactno': '6786786789'
            },
            'address': {
                'postalcode': '15469',
                'city': 'NORMALVILLE',
                'streetaddress1': 'NORMALVILLE',
                'country': 'US',
                'stateorprovince': 'PA'
            },
            'siteid': '',
            'sitetype': 'shipto'
        }
    ],
    'organization': {
        'address': {
            'postalcode': '15469',
            'country': 'US',
            'streetaddress1': 'NORMALVILLE',
            'city': 'NORMALVILLE',
            'stateorprovince': 'PA'
        },
        'organization': 'Icreon'
    },
    'cybercash': 'CHECK',
    'shippingcode': '',
    'tax': '46.62',
    'grandtotal': '305.62',
    'cardtype': 'visa',
    'ponumber': 'none',
    'expirydate': '2022/12/03',
    'customers': [
        {
            'contact': {
                'firstname': 'Prabhakar',
                'contactno': '6786786789',
                'email': 'prabhakar.mishra@test.org',
                'lastname': 'Mishra'
            },
            'siteid': '',
            'typeofcustomer': 'Employee',
            'address': {
                'stateorprovince': 'PA',
                'city': 'NORMALVILLE',
                'country': 'US',
                'streetaddress1': 'NORMALVILLE',
                'postalcode': '15469'
            }
        }
    ]
}"
                  );
            string jsonobj = new JavaScriptSerializer().Serialize(addOrderObj);
            return jsonobj;
        }

        private static string ValidateAddressApiPayload()
        {
            // address validation
            var addressValidationObj = new JavaScriptSerializer().Deserialize<dynamic>(@"{
                         'streetaddress1': 'Street Address 1',
                          'streetaddress2': '',
                          'streetaddress3': 'Street Address',
                          'streetaddress4': '',
                          'city': 'WINDSOR',
                          'stateorprovince': 'ON',
                          'postalcode': 'N9K CA2',
                          'country': ''
                        }");
            string jsonobj = new JavaScriptSerializer().Serialize(addressValidationObj);
            return jsonobj;
        }

        private static List<string> ReadJsonFile(string filePath)
        {
            List<string> jsonFileData = new List<string>();

            DirectoryInfo dir = new DirectoryInfo(filePath);
            try
            {
                foreach (FileInfo flInfo in dir.GetFiles())
                {

                    string path = dir.FullName + "\\" + flInfo.Name;
                    using (StreamReader r = new StreamReader(path))
                    {
                        jsonFileData.Add(r.ReadToEnd());
                        //  List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                    }

                }
                return jsonFileData;
                //using (StreamReader r = new StreamReader(filePath))
                //{
                //    json = r.ReadToEnd();
                //    //  List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                //}
                ////return json;
            }
            catch (Exception ex)
            {
                DriverContext.ExtentStepTest.Log(LogStatus.Info, "ReadJsonFile : Failed due to  error " + ex.Message);
                return null;
            }
            ;
        }

        private static string OrderHistoryApiPayload()
        {
            //var fromDate = DateTime.UtcNow.AddDays(-30); // new DateTime(2001, 01, 05, 0, 0, 0);
            //var toDate = DateTime.UtcNow;
            //var order = new Order { Paging = new Paging { PageIndex = 1, PageSize = 100}, Filter = new Filter { AccountNumber = "1775460", DateFrom =fromDate, DateTo=toDate } };
            //string jsongStr = new JavaScriptSerializer().Serialize(order);

            string jsongStr = "{\"Filter\":{\"DateFrom\":\"\\/Date(1548997200000)\\/\",\"DateTo\":\"\\/Date(1564632000000)\\/\",\"AccountNumber\":\"1775460\"}," +
                " \"Paging\":{ \"PageSize\":100,\"PageIndex\":1}}";

            return jsongStr;



            //string jsongStr = "{\"Filter\":{\"DateFrom\":\"2019-02-01T00:00:00Z\",\"DateTo\":\"2019-08-02T11:30:25Z\",\"AccountNumber\":\"1872774\"}," +
            //    " \"Paging\":{ \"PageSize\":100,\"PageIndex\":1}}";

            //string jsongStr = @"{""Filter"":{""DateFrom"":""2019-07-01T00:00:00+0530"",""DateTo"":""2019-08-01T00:00:00+0530"",""AccountNumber"":""1872774""},""Paging"":{""PageSize"":100,""PageIndex"":1}}";
            //string jsonobj = new JavaScriptSerializer().Serialize(test);
            //var orderhistory = new JavaScriptSerializer().Deserialize<dynamic>(@"{
            //              'Filter': {'AccountNumber':'2120244', DateFrom:'2019-01-01T00:00:00+0530', DateTo:'2019-07-31T11:30:25+0530'},
            //              'Paging': {'PageSize':100,'PageIndex':1}
            //            }");

            //var jsonStr = @"{'Filter':\{'DateFrom':'\/Date(1548824400)\/','DateTo':'\/Date(1564459200)\/','AccountNumber':'1559790'},'Paging':{'PageSize':100,'PageIndex':1}}";
        }

        private static string GetAzureToken()
        {
            var client = new HttpClient();
            var url = "https://login.microsoftonline.com/9b80e88e-980b-4d70-a21b-773d329f445d/oauth2/token/";

            var pairs = new List<KeyValuePair<string, string>>
            {
                /*
                // dev 
                new KeyValuePair<string, string> ("resource","https://astm.org/72b79085-c072-4221-827f-b9eb92eb9d71"),
                new KeyValuePair<string, string> ("client_id","0294731a-94e8-4abb-9655-bad6d92cd187"),
                new KeyValuePair<string, string> ("client_secret","IKS/xOhvJzlPX83yI3uKBjG7wkYwMaEYxK/fsHRLkgA="),
                new KeyValuePair<string, string> ("grant_type","client_credentials")
                */
                
                
                // qa
                new KeyValuePair<string, string> ("resource","https://astm.org/4ab138c6-9707-44e1-bc39-e258f999fd25"),
                new KeyValuePair<string, string> ("client_id","6ef8f8b5-85ab-46c7-9ede-b61e904e1004"),
                new KeyValuePair<string, string> ("client_secret","PYLDKBT4/hrTdkl4yUq9ekHKoe/6nGfh63Jd+PaXoZg="),
                new KeyValuePair<string, string> ("grant_type","client_credentials")
                

                //Sales force prod***********
                /*
                new KeyValuePair<string, string> ("resource","https://astm.org/e3a87226-efa1-4f5c-83e7-196d1ff12d32"),
                new KeyValuePair<string, string> ("client_id","14b4b62e-8751-42ef-acc0-5de8afc5a958"),
                new KeyValuePair<string, string> ("client_secret","M5phgiuJbdvzXyuqgKWEzo3Kp7N7R7lexg8c5GPNx5Q="),
                new KeyValuePair<string, string> ("grant_type","client_credentials")
                */
            };

            var content = new FormUrlEncodedContent(pairs);
            var response = client.PostAsync(url, content).Result;
            string accessToken = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                string output = response.Content.ReadAsStringAsync().Result;
                AzureToken result = new JavaScriptSerializer().Deserialize<AzureToken>(output);
                accessToken = result.access_token;
            }
            return accessToken;
        }


    }

    public class AzureToken
    {
        public string token_type { get; set; }
        public string expires_in { get; set; }

        public string access_token { get; set; }
    }
}
