﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestClient
{
    public class Person
    {
        public int ID  { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Double PayRate { get; set; }
        public  DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}