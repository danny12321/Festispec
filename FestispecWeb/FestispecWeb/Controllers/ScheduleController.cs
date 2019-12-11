﻿using DHTMLX.Common;
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
    public class ScheduleController : Controller
    {
        // GET: Availabilities
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
            return (new SchedulerAjaxData(
                new FestispecEntities().Inspectors_availability.
                    Select(e => new { e.id, e.start_date, e.end_date, e.inspector_id, e.text })
                )
            );
        }
    }
}