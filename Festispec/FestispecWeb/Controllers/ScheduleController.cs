using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestispecWeb.Controllers
{
    public class ScheduleController : Controller
    {
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Terrace;
            sched.LoadData = true;
            sched.Config.isReadonly = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = DateTime.Today;
            return View(sched);
        }

        public ContentResult Data()
        {
            var uemail = (string)Session["username"];
            var entities = new FestispecEntities();
            var user = (int)entities.Users.Where(u => u.email.Equals(uemail)).Select(u => u.inspector_id).FirstOrDefault();
            var userinspectionid = entities.Inspectors_at_inspection.ToList().Where(i => i.inpector_id == user).Select(e => e.inspection_id);
            return (new SchedulerAjaxData(entities.Inspections.Select(e => new { e.id, e.start_date, e.end_date, text = e.description}).Where(e => userinspectionid.Contains(e.id)).ToList()
                )
            );
        }
    }
}
