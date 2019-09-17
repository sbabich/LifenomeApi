using LifenomeAPI.Entities;
using LifenomeAPI.Exceptions;
using LifenomeAPI.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LifenomeAPI
{
    public class LifenomeAPI
    {
        internal static string AuthUrl = 
            "https://lifenome.auth0.com/oauth/token";
        internal static string AudienceAuth = 
            "https://mylifenome-dev.lifenome.com/api/mylifenome";
        internal static string RequestRootURL =  
            "https://mylifenome-demo.lifenome.com/api/mylifenome/v2/";

        public static AuthResponse Auth { get; set; }

        /// <summary>
        /// Аутентификация
        /// </summary>
        public static void DoAuth()
        {
            AuthRequest ar = AuthRequest.GetDefault();
            Auth = SendRequest<AuthResponse>(AuthUrl, ar);
        }

        /// <summary>
        /// Lists genotype profiles.
        /// </summary>
        /// <returns></returns>
        public static List<Profile> GetProfiles()
        {
            //GET /profiles
            return ExecProc<List<Profile>>("profiles", "GET");
        }

        /// <summary>
        /// Creates new genotype profile.
        /// </summary>
        /// <param name="profile"></param>
        public static Profile CreateNewProfile(String first_name, String last_name, String genotype_file_path)
        {
            CheckAuth();

            String url = RequestRootURL + "profiles";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("first_name", first_name);
            nvc.Add("last_name", last_name);
            NameValueCollection files = new NameValueCollection();
            if (!String.IsNullOrEmpty(genotype_file_path))
            {
                files.Add("genotype.genotype_file", genotype_file_path);
            }
            String resp = SendRequest(url, nvc, files);

            Profile res = APIResponse.Parse<Profile>(resp);
            return res;
        }

        /// <summary>
        /// Get a profile info for a specific profile.
        /// </summary>
        /// <param name="profile_id"></param>
        /// <returns></returns>
        public static Profile GetProfile(int profile_id)
        {
            //GET /profiles/{profile_id}
            return ExecProc<Profile>("profiles/" + profile_id, "GET");
        }

        /// <summary>
        /// Updates existing genotype profile. Only basic properties of the profile can be updated (first_name, last_name, gender, date_of_birth nad ethnicity).
        /// </summary>
        /// <param name="profile"></param>
        public static Profile UpdateProfile(Profile profile)
        {
            Profile _profile_data = profile.Clone() as Profile;

            _profile_data.Id = 0;
            _profile_data.Links = null;
            _profile_data.Genotype = null;

            //PUT /profiles/{profile_id}
            return ExecProc<Profile>("profiles/" + profile.Id,
                _profile_data,
                "PUT");
        }

        /// <summary>
        /// Deletes existing genotype profile.
        /// </summary>
        /// <param name="profile_id"></param>
        /// <returns></returns>
        public static void DeleteProfile(int profile_id)
        {
            //DELETE /profiles/{profile_id}
            ExecProc<String>("profiles/" + profile_id, "DELETE");
        }

        /// <summary>
        /// Imports genotype data from 23andMe service.
        /// </summary>
        public static void ImportGenotypeData(int profile_id)
        {
            //PUT /profiles/{profile_id}/genotype/import/23andme
            ImportResponse res = ExecProc<ImportResponse>(
                String.Format("profiles/{0}/genotype/import/23andme", profile_id),
                new ImportRequest()
                {
                    AccessToken  = "ccea40d57c453af5d1d80daefd4036da",
                    ProfileId = "f4c9324568250f71",
                    RefreshToken = "f7c5da6f179e823a68b35b7da067e494",
                }, 
                "PUT");
        }

        /// <summary>
        /// List reports activated for the profile.
        /// </summary>
        /// <param name="profile_id"></param>
        /// <returns></returns>
        public static List<Report> GetReports(int profile_id)
        {
            //GET /profiles/{profile_id}/reports
            return ExecProc<List<Report>>(String.Format("profiles/{0}/reports", profile_id), 
                "GET");
        }

        public static List<Report> GetReports123(String mmm)
        {
            //GET /profiles/{profile_id}/reports
            return ExecProc<List<Report>>(mmm,
                "GET");
        }

        /// <summary>
        /// Report.
        /// </summary>
        /// <returns></returns>
        public static Report GetReport(int profile_id, String report_code)
        {
            //GET /profiles/{profile_id}/reports/{report_code}
            return ExecProc<Report>(String.Format("profiles/{0}/reports/{1}", profile_id, report_code), 
                "GET");
        }

        /// <summary>
        /// Report scores. Get complete report: all sections with all trait scores.
        /// </summary>
        /// <returns></returns>
        public static List<Report> GetReportScores(int profile_id, String report_code)
        {
            //GET /profiles/{profile_id}/reports/{report_code}/scores
            return ExecProc<List<Report>>(String.Format("profiles/{0}/reports/{1}/scores", profile_id, report_code), 
                "GET");
        }

        /// <summary>
        /// Report predispositions. Show list of TraitScore objects which have assessment > 0.
        /// </summary>
        /// <returns></returns>
        public static List<ReportSectionEntry> GetReportPredispositions(int profile_id, String report_code)
        {
            //GET /profiles/{profile_id}/reports/{report_code}/predispositions
            return ExecProc<List<ReportSectionEntry>>(String.Format("profiles/{0}/reports/{1}/predispositions", profile_id, report_code), 
                "GET");
        }

        /// <summary>
        /// Report section.
        /// </summary>
        /// <returns></returns>
        public static ReportSection GetReportSection(int profile_id, String report_code, String section_code)
        {
            //GET /profiles/{profile_id}/reports/{report_code}/sections/{section_code}
            return ExecProc<ReportSection>(String.Format("profiles/{0}/reports/{1}/sections/{2}", profile_id, report_code, section_code), 
                "GET");
        }

        /// <summary>
        /// Retrieve PDF report.
        /// </summary>
        /// <returns></returns>
        public static void RetrieveReportPDF(int profile_id, String report_code, String file_name_pdf)
        {
            CheckAuth();

            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Add("Authorization", String.Format("{0} {1}", Auth.TypeToken, Auth.AccessToken));

            //GET /profiles/{profile_id}/reports/{report_code}/pdf
            Stream stream = SendRequestStream(
                String.Format("{2}profiles/{0}/reports/{1}/pdf", profile_id, report_code, RequestRootURL), 
                null,
                "GET", 
                headers, 
                "application/json", 
                300000);

            byte[] buffer = new byte[65536];
            using (FileStream file = File.Create(file_name_pdf))
            {
                int num;
                while ((num = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    file.Write(buffer, 0, num);
                }
            }
        }

        /// <summary>
        /// Create PDF report.
        /// </summary>
        /// <returns></returns>
        public static StatusResponse CreateReportPDF(int profile_id, String report_code, String callback_point)
        {
            //POST /profiles/{profile_id}/reports/{report_code}/pdf
            return ExecProc<StatusResponse>(String.Format("profiles/{0}/reports/{1}/pdf", profile_id, report_code),
                new CreateReportPDFRequest
                {
                    Callback = callback_point,
                },
                "POST");
        }

        /// <summary>
        /// Recommends fitness exercises.
        /// </summary>
        /// <returns></returns>
        public static List<RecommendFitness> GetRecommendsFitness(int profile_id)
        {
            //GET /profiles/{profile_id}/recommendations/fitness
            return ExecProc<List<RecommendFitness>>(String.Format("profiles/{0}/recommendations/fitness", profile_id), 
                "GET");
        }

        /// <summary>
        /// Recommends groceries.
        /// </summary>
        /// <returns></returns>
        public static List<RecommendGroceries> GetRecommendsGroceries(int profile_id)
        {
            //GET /profiles/{profile_id}/recommendations/groceries
            return ExecProc<List<RecommendGroceries>>(String.Format("profiles/{0}/recommendations/groceries", profile_id),
                "GET");
        }

        /// <summary>
        /// Recommends recipes.
        /// </summary>
        /// <returns></returns>
        public static List<RecommendsRecipes> GetRecommendsRecipes(int profile_id)
        {
            //GET /profiles/{profile_id}/recommendations/recipes
            return ExecProc<List<RecommendsRecipes>>(String.Format("profiles/{0}/recommendations/recipes", profile_id),
                "GET");
        }

        /// <summary>
        /// Recommends skin products.
        /// </summary>
        /// <returns></returns>
        public static List<RecommendsScinProducts> GetRecommendsScinProducts(int profile_id)
        {
            //GET /profiles/{profile_id}/recommendations/skingenie
            return ExecProc<List<RecommendsScinProducts>>(String.Format("profiles/{0}/recommendations/skingenie", profile_id),
                "GET");
        }

        /// <summary>
        /// Recommended skin products from survey (PUBLIC)
        /// </summary>
        /// <returns></returns>
        public static RecommendsSkinProductsSurvey GetRecommendsSkinProductsSurvey(String recommender_code)
        {
            //Check data
            if (String.IsNullOrEmpty(recommender_code) || !recommender_code.Equals("skingenie"))
                throw new APIInvalidValueException("Valid value for parameter \"recommender_code\" is only \"skingenie\"");

            //GET /recommendations/{recommender_code}/survey
            return ExecProc<RecommendsSkinProductsSurvey>(String.Format("recommendations/{0}/survey", recommender_code),
                "GET");
        }

        /// <summary>
        /// Item categories.
        /// </summary>
        /// <returns></returns>
        public static List<Category> GetRecommendsCategory(String recommender_code)
        {
            //Check data
            if (String.IsNullOrEmpty(recommender_code) 
                || !(recommender_code.Equals("skingenie") 
                || recommender_code.Equals("fitness") 
                || recommender_code.Equals("groceries")))
                throw new APIInvalidValueException("Valid values for parameter \"recommender_code\" is \"skingenie\", \"fitness\" or \"groceries\"");

            ///GET /recommendations/{recommender_code}/categories
            return ExecProc<List<Category>>(String.Format("recommendations/{0}/categories", recommender_code),
                "GET");
        }

        /// <summary>
        /// Item tags.
        /// </summary>
        /// <returns></returns>
        public static List<Tag> GetRecommendsTag(String recommender_code)
        {
            //Check data
            if (String.IsNullOrEmpty(recommender_code)
                   || !(recommender_code.Equals("skingenie")
                   || recommender_code.Equals("fitness")
                   || recommender_code.Equals("groceries")))
                throw new APIInvalidValueException("Valid values for parameter \"recommender_code\" is \"skingenie\", \"fitness\" or \"groceries\"");

            ///GET /recommendations/{recommender_code}/tags
            return ExecProc<List<Tag>>(String.Format("recommendations/{0}/tags", recommender_code),
                "GET");
        }

        /// <summary>
        /// Item details.
        /// </summary>
        /// <returns></returns>
        public static ProductDetail GetRecommendsDetail(String recommender_code, String product_id)
        {
            //Check data
            if (String.IsNullOrEmpty(recommender_code) || !recommender_code.Equals("skingenie"))
                throw new APIInvalidValueException("Valid value for parameter \"recommender_code\" is only \"skingenie\"");

            ///GET /recommendations/{recommender_code}/product/{product_id}
            return ExecProc<ProductDetail>(String.Format("recommendations/{0}/product/{1}", recommender_code, product_id),
                "GET");
        }

        //--- Backend ---

        public static void CheckAuth()
        {
            if (Auth == null)
                DoAuth();
        }

        private static T ExecProc<T>(
            String proc,
            String method = "POST",
            String content_type = "application/json",
            int timeout = 30000)
        {
            return ExecProc<T>(proc, null, method, content_type, timeout);
        }

        private static T ExecProc<T>(
            String proc, 
            Object request, 
            String method = "POST", 
            String content_type = "application/json", 
            int timeout = 30000)
        {
            CheckAuth();

            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Add("Authorization", String.Format("{0} {1}", Auth.TypeToken, Auth.AccessToken));

            return SendRequest<T>(RequestRootURL + proc, request, method, headers, content_type, timeout);
        }

        private static T SendRequest<T>(
            String url, 
            Object request, 
            String method = "POST", 
            Dictionary<String, String> headers = null, 
            String content_type = "application/json", 
            int timeout = 30000)
        {
            String json_post = request != null 
                ? JsonHelper.GetJson(request, request.GetType())
                : null;

            String resp = SendRequest(url, json_post, method, headers, content_type, timeout);

            if (!url.Contains("token"))
            {
                String fn = url.Replace(RequestRootURL, "").Replace("/", "--");
                using (FileStream fs = File.OpenWrite(String.Format(@".\response={1}-{0}.txt", fn, method)))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(resp);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            if (String.IsNullOrEmpty(resp))
                return default(T);

            T res = APIResponse.Parse<T>(resp);
            (res as APIResponse)?.CheckDataAndThrowIfHasError();
            return res;
        }

        private static String SendRequest(String url,
            String json_post,
            String method,
            Dictionary<String, String> headers,
            String content_type,
            int timeout)
        {
            Stream stream = SendRequestStream(url, json_post, method, headers, content_type, timeout);
            return new StreamReader(stream, Encoding.UTF8).ReadToEnd();
        }

        private static Stream SendRequestStream(
            String url, 
            String json_post, 
            String method, 
            Dictionary<String, String> headers, 
            String content_type, 
            int timeout)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = method;
            httpWebRequest.ContentType = content_type;
            httpWebRequest.ReadWriteTimeout = timeout;

            if (headers != null)
            {
                httpWebRequest.PreAuthenticate = true;
                foreach (String key in headers.Keys)
                    httpWebRequest.Headers.Add(key, headers[key]);
            }

            if (!String.IsNullOrEmpty(json_post))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(json_post);
                httpWebRequest.ContentLength = (long)bytes.Length;
                httpWebRequest.GetRequestStream().Write(bytes, 0, bytes.Length);
                httpWebRequest.GetRequestStream().Close();
            }

            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                return httpWebResponse.GetResponseStream();
            }
            catch (WebException ex)
            {
                throw ThrowWebException(ex);
            }
        }

        private static string SendRequest(string url, NameValueCollection values, NameValueCollection files = null)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            // The first boundary
            byte[] boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            // The last boundary
            byte[] trailer = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            // The first time it itereates, we need to make sure it doesn't put too many new paragraphs down or it completely messes up poor webbrick
            byte[] boundaryBytesF = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

            // Create the request and set parameters
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", String.Format("{0} {1}", Auth.TypeToken, Auth.AccessToken));
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;

            // Get request stream
            Stream requestStream = request.GetRequestStream();

            foreach (string key in values.Keys)
            {
                // Write item to stream
                byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}", key, values[key]));
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                requestStream.Write(formItemBytes, 0, formItemBytes.Length);
            }

            if (files != null)
            {
                foreach (string key in files.Keys)
                {
                    String fn = files[key];
                    if (File.Exists(fn))
                    {
                        int bytesRead = 0;
                        byte[] buffer = new byte[2048];
                        byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", key, files[key]));
                        requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                        requestStream.Write(formItemBytes, 0, formItemBytes.Length);

                        using (FileStream fileStream = new FileStream(files[key], FileMode.Open, FileAccess.Read))
                        {
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                // Write file content to stream, byte by byte
                                requestStream.Write(buffer, 0, bytesRead);
                            }

                            fileStream.Close();
                        }
                    }
                }
            }

            // Write trailer and close stream
            requestStream.Write(trailer, 0, trailer.Length);
            requestStream.Close();

            try
            {
                using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    return reader.ReadToEnd();
                };
            }
            catch (WebException ex)
            {
                throw ThrowWebException(ex);
            }
        }

        private static APIException ThrowWebException(WebException ex)
        {
            using (var stream = ex.Response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                String error = reader.ReadToEnd();
                APIException ex_new = new APIException(error, ex);
                return ex_new;
            }
        }
    }
}
