using Castle.Windsor;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.Facades;
using HospitalIS.BusinessLayer.Facades.Common;
using HospitalIS.WebApplication.Models;
using HospitalIS.WebApplication.Models.Disease;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HospitalIS.WebApplication.Controllers
{
    public class DiseaseController : Controller
    {
        public DiseaseFacade DiseaseFacade { get; set; }

        public SympthomFacade SympthomFacade { get; set; }

        //private async Task<Guid> CreateNonExistingDisease(AddDiseaseModel addDiseaseModel)
        //{
        //    var disease = new DiseaseDto { Name = addDiseaseModel.Name, DiseaseToSympthomDtos = new List<DiseaseToSympthomDto>() };
        //    var guid = await DiseaseFacade.CreateDiseaseAsync(disease);
        //    return guid;
        //}

        //private async Task<List<Guid>> CreateNonExistingSympthoms(List<SympthomDto> sympthomDtos)
        //{
        //    var sympthomGuids = new List<Guid>();
        //    foreach (var sympthomDto in sympthomDtos)
        //    {
        //        sympthomGuids.Add(await SympthomFacade.CreateSympthomAsync(sympthomDto));
        //    }

        //    return sympthomGuids;
        //}

        public ActionResult DiseaseBySympthoms()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DiseaseBySympthoms(DiseaseBySympthomsModel diseaseGetBySympthomsModel) //rozbite, fixnut
        {
            if (!ModelState.IsValid) return View(diseaseGetBySympthomsModel);
            var sympthoms = diseaseGetBySympthomsModel.Sympthoms.Split(',');

            List<DiseaseInfoModel> modelDiseases = new List<DiseaseInfoModel>();
            var diseases = await DiseaseFacade.GetDiseasesBySympthomsAsync(sympthoms);

            foreach (var disease in diseases)
            {
                modelDiseases.Add(new DiseaseInfoModel { Name = disease });
            }

            return View("FilteredDiseasesView", modelDiseases);
        }

    }

}