//https://my.lifenome.com/docs/api/mylifenome/v2#section/Authentication/Authentication-service

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection;
using LifenomeAPI.Entities;
using System.IO;
using System.Net.Http.Headers;
using LifenomeAPI.AndMe23API;

namespace LifenomeAPI
{
    class Program
    {
        /*public static async void test23andme()
        {
            using (var client = new HttpClient())
            {
                String addr = "https://api.23andme.com/token";
                client.BaseAddress = new Uri(addr);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("client_id", "0c27ba33a68c8aaab888f16a3e1df61a"));
                postData.Add(new KeyValuePair<string, string>("client_secret", "779f88f970acba4d084d3c56bd0addaa"));
                postData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                postData.Add(new KeyValuePair<string, string>("code", "b05e8597cde866bddf5038f7d3570859"));
                postData.Add(new KeyValuePair<string, string>("redirect_uri", "https://www.ababilova.com/getpost.php"));
                postData.Add(new KeyValuePair<string, string>("scope", "report:all"));

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync(addr, content);

                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);
        }
        }*/

        private static string dnafile = @"d:\23andme.data\genome_Igor_Iofinov_v4_Full_20180326063026.txt";
        

        static void Main(string[] args)
        {
            //API.GetAuthorizationCode();

            //test23andme();
            //String ret = "[{\"id\":798,\"first_name\":\"Igor\",\"last_name\":\"Iofinov\",\"gender\":\"m\",\"date_of_birth\":null,\"ethnicity\":\"white\",\"genotype\":null,\"links\":[{\"href\":\"https://mylifenome-demo.lifenome.com/api/mylifenome/v2/profiles/798\",\"rel\":\"self\"},{\"href\":\"https://mylifenome-demo.lifenome.com/api/mylifenome/v2/profiles/798/reports\",\"rel\":\"reports\"},{\"href\":\"https://mylifenome-demo.lifenome.com/api/mylifenome/v2/profiles/798/recommendations\",\"rel\":\"recommendations\"}]}]";
            //List<Profile> res = JsonHelper.Parse<List<Profile>>(ret);

            /*byte[] buff = File.ReadAllBytes(@"c:\Data\Projects\cs2\NY\genome_ii_v4_Full_20180326063026.zip");
            String file = Convert.ToBase64String(buff);

            Profile prof = new Profile
            {
                FirstName = "Sergei",
                LastName = "Babich",
                Gender = "m",
                Ethnicity = "white",
                DateOfBirth = "13/08/1986",
                Genotype = new Genotype
                {
                    Format = "23andMe",
                    GenotypeFile = file,
                }
            };*/

            try
            {
                String genotype_file = @"c:\Data\Projects\cs2\NY\genome_Natasha_Konstorum_v4_Full_20171110085440.zip";
                Profile prof_new = LifenomeAPI.CreateNewProfile("Natasha123", "Konstorum123", genotype_file);
                LifenomeAPI.ImportGenotypeData(prof_new.Id);
            }
            catch
            { }

            Uniso.Log.IsDebug = true;

            List<Profile> res = LifenomeAPI.GetProfiles();
            List<Report> reports = LifenomeAPI.GetReports(res[8].Id);

            //Report r = LifenomeAPI.GetReport(res[1].Id, "allergies-and-sensitivities");

            foreach (Report r in reports)
            {
                List<Report> rs = LifenomeAPI.GetReportScores(res[8].Id, r.Code);
            }
            
            Profile profile = LifenomeAPI.GetProfile(res[1].Id);
            profile.DateOfBirth = "1980-04-24";
            profile.Gender = "m";
            profile.Ethnicity = "white";
            LifenomeAPI.UpdateProfile(profile);

            LifenomeAPI.DeleteProfile(825);

            Json.StatusResponse status1 = LifenomeAPI.CreateReportPDF(
                res[1].Id, 
                "basic-wellness", 
                "https://www.ababilova.com/oladi-s-klenovym-siropom-i-bananami");

            Json.StatusResponse status2 = LifenomeAPI.CreateReportPDF(
                res[1].Id, 
                "fitness", 
                "https://www.ababilova.com/oladi-s-klenovym-siropom-i-bananami");

            Json.StatusResponse status3 = LifenomeAPI.CreateReportPDF(
                res[1].Id, 
                "nutrition-and-metabolism", 
                "https://www.ababilova.com/oladi-s-klenovym-siropom-i-bananami");

            Json.StatusResponse status4 = LifenomeAPI.CreateReportPDF(
                res[1].Id, 
                "personality", 
                "https://www.ababilova.com/oladi-s-klenovym-siropom-i-bananami");

            Json.StatusResponse status5 = LifenomeAPI.CreateReportPDF(
                res[1].Id, 
                "skin-care", 
                "https://www.ababilova.com/oladi-s-klenovym-siropom-i-bananami");

            List<Report> ami = LifenomeAPI.GetReports(res[8].Id);
            foreach (Report re in ami)
            {
                LifenomeAPI.RetrieveReportPDF(res[1].Id, re.Code, String.Format(@".\report-{1}-{0}.pdf", re.Code, res[3].Id));
            }
            
            List<Tag> it = LifenomeAPI.GetRecommendsTag("fitness");
            List<Category> ic = LifenomeAPI.GetRecommendsCategory("fitness");

            foreach (Report re in ami)
                LifenomeAPI.GetReport(res[1].Id, re.Code);

            foreach (Report re in ami)
                LifenomeAPI.GetReportPredispositions(res[1].Id, re.Code);

            foreach (Report re in ami)
                LifenomeAPI.GetReportSection(res[1].Id, re.Code, "food_allergies");

            List<RecommendFitness> rr = LifenomeAPI.GetRecommendsFitness(res[8].Id);
            List<RecommendGroceries> rr2 = LifenomeAPI.GetRecommendsGroceries(res[8].Id);
            List<RecommendsRecipes> rr3 = LifenomeAPI.GetRecommendsRecipes(res[1].Id);            
            List<RecommendsScinProducts> rr4 = LifenomeAPI.GetRecommendsScinProducts(res[1].Id);
            RecommendsSkinProductsSurvey rssp = LifenomeAPI.GetRecommendsSkinProductsSurvey("skingenie");

            ProductDetail pd = LifenomeAPI.GetRecommendsDetail("skingenie", "P377542");

            profile.DateOfBirth = "1999-04-06";
            
            LifenomeAPI.UpdateProfile(profile);

           

            Console.ReadLine();
        }
    }
}
