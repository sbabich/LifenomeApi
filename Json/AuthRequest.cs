    using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Json
{
    [DataContract()]
    public class AuthRequest
    {
        /*values.Add(new KeyValuePair<string, string>("client_id", authObj.client_id));
            values.Add(new KeyValuePair<string, string>("client_secret", authObj.client_secret));
            values.Add(new KeyValuePair<string, string>("audience", authObj.audience));
            values.Add(new KeyValuePair<string, string>("grant_type", authObj.grant_type));
            values.Add(new KeyValuePair<string, string>("username", authObj.username));
            values.Add(new KeyValuePair<string, string>("password", authObj.password));*/

        [DataMember(Name = "client_id", Order = 0)]
        public String ClientId { get; set; }

        [DataMember(Name = "client_secret", Order = 1)]
        public String ClientSecret { get; set; }

        [DataMember(Name = "grant_type", Order = 2)]
        public String GrantType { get; set; }

        [DataMember(Name = "audience", Order = 3)]
        public String Audience { get; set; }

        [DataMember(Name = "username", Order = 4)]
        public String Username { get; set; }

        [DataMember(Name = "password", Order = 5)]
        public String Password { get; set; }

        public static AuthRequest GetDefault()
        {

            /*String json = "{\"client_id\": \"EaYwJVCI7wixNw3zZn4pTbsSMHICvw7k\", 
             * \"client_secret\": \"ZrlzDi9hb5qpD9DP1oPOm4BxilbVWbinfYj2Pke5tdtj9AjMNJ9jJNp1r1KYB7Oz\", 
             * \"grant_type\":\"password\", \"audience\": \"https://mylifenome-demo.lifenome.com/api/mylifenome\", 
             * \"username\": \"gorv692@gmail.com\", \"password\": \"marcus62#\"}";*/

            return new AuthRequest()
            {
                ClientId = "EaYwJVCI7wixNw3zZn4pTbsSMHICvw7k",
                ClientSecret = "ZrlzDi9hb5qpD9DP1oPOm4BxilbVWbinfYj2Pke5tdtj9AjMNJ9jJNp1r1KYB7Oz",
                GrantType = "password",
                Audience = "https://mylifenome-demo.lifenome.com/api/mylifenome",
                Username = "gorv692@gmail.com",
                Password = "marcus62#",
            };
        }

        public static AuthRequest GetDefault2()
        {

            /*String json = "{\"client_id\": \"EaYwJVCI7wixNw3zZn4pTbsSMHICvw7k\", 
             * \"client_secret\": \"ZrlzDi9hb5qpD9DP1oPOm4BxilbVWbinfYj2Pke5tdtj9AjMNJ9jJNp1r1KYB7Oz\", 
             * \"grant_type\":\"password\", \"audience\": \"https://mylifenome-demo.lifenome.com/api/mylifenome\", 
             * \"username\": \"gorv692@gmail.com\", \"password\": \"marcus62#\"}";*/

            return new AuthRequest()
            {
                ClientId = "6fb2ae83e0884dd0ff52d18417385dde",
                ClientSecret = "aa9fb2e0f0320fdab3c98b1738acb82d",
                GrantType = "authorization_code",
            };
        }
    }
}
