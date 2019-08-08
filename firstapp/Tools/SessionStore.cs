using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Xamarin.Forms;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Claims;
using firstapp.Interfaces;
using firstapp.ENUMS;
using System.Diagnostics;
using firstapp.Models;

namespace firstapp.Tools
{
    public class SessionStore : ISessionStore
    {
        public string UserName { get; private set; }
        public string UserPassword { get; private set; }
        public string IdToken { get; private set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime TokenIssuedServer { get; private set; }
        public DateTime TokenExpiresServer { get; private set; }
        public int ExpiresInSecounds { get; private set; }
        public string UserID { get; set; }

        private App MyApp = Application.Current as App;
        public SessionStore()
        {
            CheckSavedData();
        }

        private void CheckSavedData()
        {
            UserName = !string.IsNullOrEmpty(MyApp.AppP.UserName) ? MyApp.AppP.UserName : null;
            UserPassword = !string.IsNullOrEmpty(MyApp.AppP.Password) ? Encryption.Decrypt(MyApp.AppP.Password) : null;
            IdToken = !string.IsNullOrEmpty(MyApp.AppP.IdToken) ? Encryption.Decrypt(MyApp.AppP.IdToken) : null;
            RefreshToken = !string.IsNullOrEmpty(MyApp.AppP.RefreshToken) ? Encryption.Decrypt(MyApp.AppP.RefreshToken) : null;
            AccessToken = !string.IsNullOrEmpty(MyApp.AppP.AccessToken) ? Encryption.Decrypt(MyApp.AppP.AccessToken) : null;
            UserID = !string.IsNullOrEmpty(MyApp.AppP.UserID) ? MyApp.AppP.UserID : null;
            TokenIssuedServer = MyApp.AppP.TokenIssued;
            TokenExpiresServer = MyApp.AppP.TokenExpires;

        }
        public void PopulateSession(SignInContext sign_in, bool is_refresh = false)
        {

            IdToken = sign_in.IdToken;
            AccessToken = sign_in.AccessToken;
            TokenIssuedServer = sign_in.TokenIssued;
            TokenExpiresServer = sign_in.Expires;

            if (!is_refresh)
            {
                UserName = sign_in.UserName;
                UserPassword = sign_in.UserPassword;
                RefreshToken = sign_in.RefreshToken;

                MyApp.AppP.UserName = UserName;
                MyApp.AppP.UserID = GrabUserID(IdToken);
                MyApp.AppP.Password = Encryption.Encrypt(UserPassword);
                MyApp.AppP.RefreshToken = Encryption.Encrypt(RefreshToken);
            }


            MyApp.AppP.IdToken = Encryption.Encrypt(IdToken);
            MyApp.AppP.AccessToken = Encryption.Encrypt(AccessToken);
            MyApp.AppP.TokenIssued = TokenIssuedServer;
            MyApp.AppP.TokenExpires = TokenExpiresServer;

            Debug.WriteLine($"From:{this.GetType().Name} PopulateSession, Ir refresh:{is_refresh}, \nUserName = {UserName}\n" +
            $"UserPassword = {UserPassword}\n" +
            $"IdToken = {sign_in.IdToken}\n" +
            $"RefreshToken = {RefreshToken}\n" +
            $"AccessToken = {sign_in.AccessToken}\n" +
            $"TokenIssuedServer = {sign_in.TokenIssued}\n" +
            $"TokenExpiresServer = {sign_in.Expires}\n");
            //data to store in app 
        }

        public SessionStatus IsLoggedIn()
        {

            //Debug.WriteLine($"From:{this.GetType().Name}, == checking session data==>\n" +
            //   $"UserPassword = {UserPassword}\n" +
            //$"IdToken = {IdToken}\n" +
            //$"RefreshToken = {RefreshToken}\n" +
            //$"AccessToken = {AccessToken}\n" +
            //$"TokenIssuedServer = {TokenIssuedServer}\n" +
            //$"TokenExpiresServer = {TokenExpiresServer}\n" +
            //$"Now:{DateTime.Now}\n" +
            //$"Session is still active:{TokenExpiresServer > DateTime.Now}\n");



            bool isLoginDataPresent =
            !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrEmpty(UserPassword)
            && !string.IsNullOrEmpty(IdToken)
                && !string.IsNullOrEmpty(RefreshToken)
            && !string.IsNullOrEmpty(AccessToken)
                && TokenIssuedServer > default(DateTime)
            && TokenExpiresServer > default(DateTime);

            Debug.WriteLine($"Is login data present:{isLoginDataPresent}");

            bool isSessinStillActive = TokenExpiresServer > DateTime.Now;

            Debug.WriteLine($"Is session still active:{isSessinStillActive}");

            //TestJwtSecurityTokenHandler();
            //if login data is present but session expired, require new token from aws
            if (!isLoginDataPresent)
                return SessionStatus.LoggedOut;
            else if (isLoginDataPresent && !isSessinStillActive)
                return SessionStatus.LoggedInWithExpiredSession;
            else if (isLoginDataPresent && isSessinStillActive)
                return SessionStatus.LoggedInWithActiveSession;
            else
                return SessionStatus.LoggedOut;

        }
        private void StartRefreshSession(Action _CallBackNeedRefresh)
        {
            var _user = new UserAuthInfoObject
            {
                Email = UserName,
                Password = UserPassword,
                IdToken = IdToken,
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                TokenIssued = TokenIssuedServer,
                TokenExpire = TokenExpiresServer,
                AuthType = AuthType.RefreshSession
            };

            //MyApp.Server.SendServerRequest(
            //    () =>
            //    {
            //        //timeout action
            //    },
            //    (string msg, object e) =>
            //    {
            //        //success action
            //        _CallBackNeedRefresh();
            //    },
            //    (string msg) =>
            //    {
            //        //fail action

            //    },
            //    _user, ServerAction.NullVal, false, null, null, null, true
            //);
        }
        public void LogOut()
        {
            UserPassword = UserName = IdToken = AccessToken = RefreshToken = string.Empty;
            TokenIssuedServer = TokenExpiresServer = default(DateTime);
            MyApp.AppP.ResetSessionData();
            //ExpiresInSeconds = 0;
        }

        public UserAuthInfoObject UserObject()
        {
            return new UserAuthInfoObject
            {
                Email = UserName,
                Password = UserPassword,
                IdToken = IdToken,
                AccessToken = AccessToken,
                RefreshToken = RefreshToken,
                TokenIssued = TokenIssuedServer,
                TokenExpire = TokenExpiresServer,
            };
        }

        private string GrabUserID(string _id_token)
        {
            string _userid = null;

            //var handler = new JsonWebToken();
            var jwtHandler = new JwtSecurityTokenHandler();
            if (jwtHandler.CanReadToken(_id_token))
            {
                var token = jwtHandler.ReadJwtToken(_id_token);

                //Extract the headers of the JWT
                var headers = token.Header;
                var jwtHeader = "{";
                foreach (var h in headers)
                {
                    jwtHeader += '"' + h.Key + "\":\"" + h.Value + "\",";
                }
                jwtHeader += "}";
                //txtJwtOut.Text = "Header:\r\n" + JToken.Parse(jwtHeader).ToString(Formatting.Indented);
                Debug.WriteLine("Header:\r\n" + JToken.Parse(jwtHeader).ToString(Formatting.Indented));

                //Extract the payload of the JWT
                var claims = token.Claims;
                var jwtPayload = "{";
                foreach (Claim c in claims)
                {
                    jwtPayload += '"' + c.Type + "\":\"" + c.Value + "\",";
                    if (c.Type == "sub")
                    {
                        _userid = c.Value;
                        Debug.WriteLine($"the user id will be:{c.Value}, length:{c.Value.Length}");

                    }
                }
                jwtPayload += "}";
                //txtJwtOut.Text += "\r\nPayload:\r\n" + JToken.Parse(jwtPayload).ToString(Formatting.Indented);
                Debug.WriteLine("\r\nPayload:\r\n" + JToken.Parse(jwtPayload).ToString(Formatting.Indented));
            }
            else
            {
                Debug.WriteLine($"Id token doesnt seem to be valid");
            }

            return _userid;
        }
    }
}