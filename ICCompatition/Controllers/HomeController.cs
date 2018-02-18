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
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetList()
        {
            List<ExerciseViewModel> Result = new List<ExerciseViewModel>();
            Result = repository.GetAll().ToList();

            return Json(new { data = Result}, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddExercise(ExerciseViewModel model)
        {
            var result = false;
            if (ValidRecord(model))
            {
                return Json(new { result = false, responseText = "This type of exercise has been done today" },
                                    JsonRequestBehavior.AllowGet);
            }
            model.Id = Repository<ExerciseViewModel>.Records.Count + 100;
            repository.Insert(model);
            result = true;
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

            return result;
        }

        public JsonResult GetListById(int Id)
        {
            List<ExerciseViewModel> value = new List<ExerciseViewModel>();
            value = repository.Get(Id).ToList();

            return Json(value, JsonRequestBehavior.AllowGet);
        }


    }
}