using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalIS.WebApplication.Models.Disease
{
    public class AllDiseasesModel
    {
        public IEnumerable<string> Diseases { get; set; }
        public string SelectedDisease { get; set; }
    }
}