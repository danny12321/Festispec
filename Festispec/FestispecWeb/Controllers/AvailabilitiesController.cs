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
    public class AvailabilitiesController : Controller
    {
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
            var uemail = (string)Session["username"];
            var entities = new FestispecEntities();
            var user = (int)entities.Users.Where(u => u.email.Equals(uemail)).Select(u => u.inspector_id).FirstOrDefault();
            return (new SchedulerAjaxData(entities.Inspectors_availability.
                    Select(e => new { e.id, e.start_date, e.end_date, e.inspector_id, e.text }).Where(e => e.inspector_id == user)
                )
            ) ;
        } 

        public ContentResult Save(int? id, FormCollection actionValues)
        {
            var action = new DataAction(actionValues);
            var changedEvent = DHXEventsHelper.Bind<Inspectors_availability>(actionValues);
            
            var entities = new FestispecEntities();
            var uemail = (string)Session["username"];
              
            try
            {
                switch (action.Type)
                {
                    case DataActionTypes.Insert:
                        changedEvent.inspector_id = (int)entities.Users.Where(u => u.email.Equals(uemail)).Select(u => u.inspector_id).FirstOrDefault();
                        entities.Inspectors_availability.Add(changedEvent);
                        break;
                    case DataActionTypes.Delete:
                        changedEvent = entities.Inspectors_availability.FirstOrDefault(ev => ev.id == action.SourceId);
                        entities.Inspectors_availability.Remove(changedEvent);
                        break;
                    default:// "update"
                        var eventToUpdate = entities.Inspectors_availability.SingleOrDefault(ev => ev.id == action.SourceId);
                        eventToUpdate.id = changedEvent.id;
                        eventToUpdate.start_date = changedEvent.start_date;
                        eventToUpdate.end_date = changedEvent.end_date;
                        eventToUpdate.text = changedEvent.text;
                        entities.SaveChanges();
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