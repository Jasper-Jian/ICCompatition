using ICCompatition.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ICCompatition.Controllers
{
    public class HomeController : Controller
    {
        public Repository<ExerciseViewModel> repository = new Repository<ExerciseViewModel>();
        public List<ExerciseViewModel> Elist { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList()
        {
            List<object> Result = new List<object>();
            foreach (var record in Repository<ExerciseViewModel>.Records)
            {
                Result.Add(
                    new
                    {
                        record.Id,
                        record.ExerciseName,
                        ExerciseDateTime = record.ExerciseDateTime.ToString("yyyy-MM-dd"),
                        record.DurationInMinutes
                    }
              );
            }

            //List<ExerciseViewModel> Result = new List<ExerciseViewModel>();
            //Result = repository.GetAll().ToList();

            return Json(new { data = Result}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListById(int Id)
        {
            ExerciseViewModel model = Elist.Where(x => x.Id == Id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            //List<ExerciseViewModel> Result = new List<ExerciseViewModel>();
            //Result = repository.Get(Id).ToList();

            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddExercise(ExerciseViewModel model)
        {
            var result = false;
                if (model.Id > 0)
                {
                    ExerciseViewModel exe = Elist.SingleOrDefault(x => x.Id == model.Id);
                    exe.ExerciseName = model.ExerciseName;
                    exe.ExerciseDateTime = model.ExerciseDateTime;
                    exe.DurationInMinutes = model.DurationInMinutes;
                    result = true;
                }
                else
                {
                    if (!ValidRecord(model))
                    {
                        return Json(new { result = false, responseText = "This type of exercise has been done today" },
                                            JsonRequestBehavior.AllowGet);
                    }
                    model.Id = Repository<ExerciseViewModel>.Records.Count+100;
                    //Repository<ExerciseViewModel>.Records.Add(model);
                    repository.Insert(model);   
                    result = true;          
                }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private bool ValidRecord(ExerciseViewModel record)
        {
            /*
             *A specific exercise (ie. exerciseName) can only be entered once on a single day.
             *This constraint only needs to be enforced on the server. The mechanism used to
             *enforce this constraint is at your own discretion.
             */

            var result = Repository<ExerciseViewModel>.Records.Any(r =>
                r.ExerciseName == record.ExerciseName && r.ExerciseDateTime == record.ExerciseDateTime);

            return !result;
        }

    }
}