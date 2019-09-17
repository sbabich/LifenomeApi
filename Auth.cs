using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI
{
    internal class AuthRequest_Obj
    {
        internal string client_id;
        internal string client_secret;
        internal string audience;
        internal string grant_type;
        internal string username;
        internal string password;
    }
    
    internal class Auth
    {
        internal string client_id { get; set; }
        internal string client_secret { get; set; }

        internal Auth()
        {
            this.client_id = "EaYwJVCI7wixNw3zZn4pTbsSMHICvw7k";
            this.client_secret = "ZrlzDi9hb5qpD9DP1oPOm4BxilbVWbinfYj2Pke5tdtj9AjMNJ9jJNp1r1KYB7Oz";
            AuthRequest_Obj authReq = new AuthRequest_Obj();
            authReq.client_id = this.client_id;
            authReq.client_secret = client_secret;
            authReq.audience = Processor.AudienceAuth;
            authReq.grant_type = "password";
            authReq.username = "Resource Owner's identifier - Lifenome user identifier.";
            authReq.password = "Owner's password - Lifenome user password";
        }
    }
}
