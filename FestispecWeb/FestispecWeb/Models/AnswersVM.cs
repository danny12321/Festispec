﻿using Festipec.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FestispecWeb.Models
{
    public class AnswersVM
    {
        public Questions Question { get; set; }

        public Answers[] Answers { get; set; } = new Answers[900];

        public int question_id { get; set; }
    }
}