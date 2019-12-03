using DHTMLX.Common;
using DHTMLX.Scheduler;
using DHTMLX.Scheduler.Data;
using Festipec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestispecWeb.Controllers
{
    public class AvailabilitiesController : Controller
    {
        // GET: Availabilities
        public ActionResult Index()
        {
            var sched = new DHXScheduler(this);
            sched.Skin = DHXScheduler.Skins.Terrace;
            sched.LoadData = true;
            sched.EnableDataprocessor = true;
            sched.InitialDate = DateTime.Today;
            return View(sched);
        }

        public ContentResult Data()
        {
            //return (new SchedulerAjaxData(
            //new FestispecEntities().Inspectors_availability.
            //Select(e => new { e.id, e.text, e.start_date, e.end_date, e.inspector_id })
            //)
            //    );
            return null;
        }

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = DHXEventsHelper.Bind<Inspectors_availability>(actionValues);
            var entities = new FestispecEntities();
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        entities.Inspectors_availability.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = entities.Inspectors_availability.FirstOrDefault(ev => ev.id == action.SourceId);
                        entities.Inspectors_availability.Remove(changedEvent);
                        break;
                    default:// "update"
                        var target = entities.Inspectors_availability.Single(e => e.id == changedEvent.id);
                        DHXEventsHelper.Update(target, changedEvent, new List<string> { "id" });
                        break;
                }
                entities.SaveChanges();
                action.TargetId = changedEvent.id;
            }
            catch (Exception a)
            {
                action.Type = DataActionTypes.Error;
            }
            return (new AjaxSaveResponse(action));
        }
    }
}