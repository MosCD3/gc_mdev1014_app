using System;
using Xamarin.Forms;

namespace firstapp
{
    public class PropertiesManager
    {

        public App MyApp = Application.Current as App;


        // ==========================================
        // ==========  GETTERS/SETTERS ==============
        // ==========================================
        public string UserID
        {
            get
            {
                if (MyApp.Properties.ContainsKey("Userid"))
                    return MyApp.Properties["Userid"].ToString();
                return string.Empty;
            }
            set
            {
                if (value != null)
                {
                    MyApp.Properties["Userid"] = value;
                    MyApp.SavePropertiesAsync();
                }
                else
                {
                    MyApp.Properties.Remove("Userid");
                }
            }
        }
        public string UserName
        {
            get
            {
                if (MyApp.Properties.ContainsKey("Username"))
                    return MyApp.Properties["Username"].ToString();
                return string.Empty;
            }
            set
            {
                if (value != null)
                {
                    MyApp.Properties["Username"] = value;
                    MyApp.SavePropertiesAsync();
                }
                else
                {
                    MyApp.Properties.Remove("Username");
                }
            }
        }
        public string Password
        {
            get
            {
                if (MyApp.Properties.ContainsKey("Password"))
                    return MyApp.Properties["Password"].ToString();
                return string.Empty;
            }
            set
            {
                if (value != null)
                {
                    MyApp.Properties["Password"] = value;
                    MyApp.SavePropertiesAsync();
                }
                else
                {
                    MyApp.Properties.Remove("Password");
                }
            }
        }
        public string IdToken
        {
            get
            {
                if (MyApp.Properties.ContainsKey("IdTokenK"))
                    return MyApp.Properties["IdTokenK"].ToString();
                return string.Empty;
            }
            set
            {
                if (value != null)
                {
                    MyApp.Properties["IdTokenK"] = value;
                    MyApp.SavePropertiesAsync();
                }
                else
                {
                    MyApp.Properties.Remove("IdTokenK");
                }
            }
        }
        public string RefreshToken
        {
            get
            {
                if (MyApp.Properties.ContainsKey("RefreshTokenK"))
                    return MyApp.Properties["RefreshTokenK"].ToString();
                return string.Empty;
            }
            set
            {
                if (value != null)
                {
                    MyApp.Properties["RefreshTokenK"] = value;
                    MyApp.SavePropertiesAsync();
                }
                else
                {
                    MyApp.Properties.Remove("RefreshTokenK");
                }
            }
        }
        public string AccessToken
        {
            get
            {
                if (MyApp.Properties.ContainsKey("AccessTokenK"))
                    return MyApp.Properties["AccessTokenK"].ToString();
                return string.Empty;
            }
            set
            {
                if (value != null)
                {
                    MyApp.Properties["AccessTokenK"] = value;
                    MyApp.SavePropertiesAsync();
                }
                else
                {
                    MyApp.Properties.Remove("AccessTokenK");
                }
            }
        }
        public DateTime TokenIssued
        {
            get
            {
                if (MyApp.Properties.ContainsKey("TokenIssuedK"))
                    return (DateTime)MyApp.Properties["TokenIssuedK"];
                return default(DateTime);
            }
            set
            {
                if (value != null)
                {
                    MyApp.Properties["TokenIssuedK"] = value;
                    MyApp.SavePropertiesAsync();
                }
                else
                {
                    MyApp.Properties.Remove("TokenIssuedK");
                }
            }
        }
        public DateTime TokenExpires
        {
            get
            {
                if (MyApp.Properties.ContainsKey("TokenExpiresK"))
                    return (DateTime)MyApp.Properties["TokenExpiresK"];
                return default(DateTime);
            }
            set
            {
                if (value != null)
                {
                    MyApp.Properties["TokenExpiresK"] = value;
                    MyApp.SavePropertiesAsync();
                }
                else
                {
                    MyApp.Properties.Remove("TokenExpiresK");
                }
            }
        }

        public void ResetSessionData()
        {
            MyApp.Properties.Remove("Username");
            MyApp.Properties.Remove("Password");
            MyApp.Properties.Remove("IdTokenK");
            MyApp.Properties.Remove("RefreshTokenK");
            MyApp.Properties.Remove("AccessTokenK");
            MyApp.Properties.Remove("TokenIssuedK");
            MyApp.Properties.Remove("TokenExpiresK");
            MyApp.SavePropertiesAsync();
        }

        //================ End User Login Info ================

        public PropertiesManager()
        {
        }
    }
}
