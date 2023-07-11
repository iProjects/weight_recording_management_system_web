using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using weight_recording_MvcWebApplication.Models;
using weight_recording_dal;

namespace weight_recording_MvcWebApplication.Controllers
{
    public class weightsController : Controller
    {
        List<weight_ui_dto> _lst_records = new List<weight_ui_dto>();
        //
        // GET: /weights/
        public ActionResult Index()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format("{0}\\{1}", "log", "error.txt"));
            Console.WriteLine(filePath);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            if (!System.IO.File.Exists(filePath))
                System.IO.File.Create(filePath);

            Console.WriteLine(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);

            return View();
        }

        public ActionResult list()
        {
            string file_dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log");
            string filePath = Path.Combine(file_dir, "error.txt");

            Console.WriteLine(filePath);

            var directory = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(directory);
            }

            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.Create(filePath);
            }

            Console.WriteLine(new System.Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath);

            weights_dto_Model _weights_dto_Model = new weights_dto_Model();

            return View("list", "~/Views/Shared/_Layout_weights.cshtml", _weights_dto_Model);
        }

        [Route("weights/create")]
        public ActionResult create()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public JsonResult get_all_active_weights_from_service()
        {
            try
            {
                _lst_records = dalutilz.lstconvertservicedtointouidto(dalutilz.get_all_active_weights_from_service());

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json(_lst_records, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public JsonResult delete_weight_from_service(string weight_id)
        {
            responsedto _responsedto = new responsedto();
            try
            {

                get_all_active_weights_from_service();

                weight_ui_dto _weight_ui_dto = _lst_records.Where(i => i.weight_id == weight_id).FirstOrDefault();
                _responsedto = dalutilz.deleteweightingservice(_weight_ui_dto);

                get_all_active_weights_from_service();

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return Json(ex.ToString(), JsonRequestBehavior.AllowGet);
            }
            return Json(_responsedto, JsonRequestBehavior.AllowGet);
        }




    }
}