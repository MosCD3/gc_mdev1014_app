using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using firstapp.ENUMS;
using firstapp.Interfaces;
using firstapp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace firstapp
{
    public class ServerConnect
    {
        public IApiCognito AuthApi => new ApiCognito();
        private App MyApp = Application.Current as App;
        //private HttpResponseMessage connectionResponse;

        public ServerConnect()
        {
        }


        async public Task<ServerReplyStatus> Connect(UserAuthInfoObject _connectInfo)
        {

            var responseCognito = new CognitoContext();
            var funcReply = ServerReplyStatus.Unknown;
            string user;
            string pass;

            switch (_connectInfo.AuthType)
            {
                case AuthType.SignUp:
                    user = _connectInfo.Email.Trim().ToLower();
                    pass = _connectInfo.Password.Trim();
                    responseCognito = await AuthApi.SignUp(user, pass);

                    switch (responseCognito.Result)
                    {
                        case CognitoResult.SignupOk:
                            Debug.WriteLine("Sign up ok");
                            //responseJson = "{\"error\":\"false\",\"message\":\"User_Created\"}";
                            funcReply = ServerReplyStatus.Success;
                            break;
                        case CognitoResult.PasswordRequirementsFailed:
                            Debug.WriteLine("Password requirment failed");
                            //responseJson = "{\"error\":\"true\",\"message\":\"Pass_Req_Failed\"}";
                            funcReply = ServerReplyStatus.PasswordRequirementsFailed;
                            break;
                        case CognitoResult.UserNameAlreadyUsed:

                            Debug.WriteLine("Email exists");
                            //responseJson = "{\"error\":\"true\",\"message\":\"Email_Exist\"}";
                            funcReply = ServerReplyStatus.UserNameAlreadyUsed;
                            break;
                        default:
                            Debug.WriteLine($"strange error: {responseCognito.Error}");
                            //responseJson = "{\"error\":\"true\",\"message\":\"" + responseCognito.Error + "\"}";
                            funcReply = ServerReplyStatus.Fail;
                            break;
                    }

                    break;





                case AuthType.SignIn:
                    user = _connectInfo.Email.Trim().ToLower();
                    pass = _connectInfo.Password.Trim();

                    responseCognito = await AuthApi.SignIn(user, pass);

                    Debug.WriteLine($" Reply from aws Auth: {responseCognito.Result} ");
                    switch (responseCognito.Result)
                    {
                        case CognitoResult.Ok:
                            //responseJson = "{\"error\":\"false\",\"message\":\"--\"}";
                            Debug.WriteLine($"From:{this.GetType().Name},Login success");
                            funcReply = ServerReplyStatus.Success;
                            //MyApp.Session.PopulateSession(responseCognito as SignInContext);
                            break;
                        case CognitoResult.NotConfirmed:
                            Debug.WriteLine($"From:{this.GetType().Name},Email not confirmed");
                            //responseJson = "{\"error\":\"true\",\"message\":\"Email_Not_Activated\"}";
                            funcReply = ServerReplyStatus.NotConfirmed;
                            break;
                        case CognitoResult.InvalidPassword:
                            Debug.WriteLine($"From:{this.GetType().Name},Invalid Password");
                            //responseJson = "{\"error\":\"true\",\"message\":\"Password_Mismatch\"}";
                            funcReply = ServerReplyStatus.InvalidPassword;
                            break;
                        case CognitoResult.UserNotFound:
                            Debug.WriteLine($"From:{this.GetType().Name},Email not found");
                            //responseJson = "{\"error\":\"true\",\"message\":\"Email_Not_Exist\"}";
                            funcReply = ServerReplyStatus.UserNotFound;
                            break;
                        default:
                            Debug.WriteLine($"strange error: {responseCognito.Error}");
                            //responseJson = "{\"error\":\"true\",\"message\":\"" + responseCognito.Error + "\"}";
                            funcReply = ServerReplyStatus.Fail;
                            break;
                    }
                    break;
            }



            return funcReply;
        }



        public async Task<ServerResponseObject> ConnectApi(object _sentObject,string _endPoint)
        {


            //var returnMessage = ServerReplyStatus.Unknown;

            var responseMessage = new ServerResponseObject();

            string the_uri;
            string jsonObject = JsonConvert.SerializeObject(_sentObject);
            var content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            the_uri = Keys.Aws_Resource_Folder + _endPoint;

            Debug.WriteLine("//Connected To Server/Device with body:" + jsonObject + "\nWith URI:" + the_uri);

            using (HttpClient _client = new HttpClient())
            {
                try
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(MyApp.Session.IdToken);

                   var connectionResponse = await _client.PostAsync(the_uri, content);

                    if (connectionResponse.IsSuccessStatusCode)
                    {
                        
                        Debug.WriteLine($"//Connection success, Status code:{connectionResponse.StatusCode}");
                        responseMessage.status = ServerReplyStatus.Success;
                        responseMessage.data =  connectionResponse.Content.ReadAsStringAsync().Result; 
                    }
                    else
                    {
                        Debug.WriteLine($"//Connection failed, Status code:{connectionResponse.StatusCode}");
                        responseMessage.status = ServerReplyStatus.Fail;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"//From {this.GetType().Name}, exception in connect:{e.Message}");
                    responseMessage.error = e.Message;
                    responseMessage.status = ServerReplyStatus.Unknown;
                }
            }

            return responseMessage;
        }

    }
}
