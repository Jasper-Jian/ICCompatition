using ICCompatition.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;

namespace ICCompatition
{
    public class RouteConfig
    {
        private static List<ExerciseViewModel> loatData()
        {
            const string filePath =
                @"c:\users\jasper\documents\visual studio 2017\Projects\ICCompatition\ICCompatition\Models\Data.json";
            using (StreamReader reader = new StreamReader(filePath))
            {
                string content = reader.ReadToEnd();
                List<ExerciseViewModel> list = JsonConvert.DeserializeObject<List<ExerciseViewModel>>(content);
                return list;
            }
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Home",
               url: "{controller}/{action}",
               defaults: new { controller = "Home", action = "Index" }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            Repository<ExerciseViewModel>.Records=loatData();
            
        }
    }
}
