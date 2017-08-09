using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using USCS.Models;
using USCS.ViewModels;

namespace USCS.Controllers
{
    public class USCSFormController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: USCSForm
        public ActionResult Index()
        {
            //return View();
            //return Redirect("/ProcedureCodes/Index");
            return View(db.ProcedureCodes.ToList());
        }

        // GET: USCSForm/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: USCSForm/Create
        public ActionResult Create()
        {
            var procedurecode = new ProcedureCode();

            procedurecode.Usages = new List<Usage>();
            PopulateAssignedUsageData(procedurecode);

            procedurecode.Populations = new List<Population>();
            PopulateAssignedPopulationData(procedurecode);

            procedurecode.Units = new List<Unit>();
            PopulateAssignedUnitData(procedurecode);

            procedurecode.ModeOfDeliveries = new List<ModeOfDelivery>();
            PopulateAssignedModeOfDeliveryData(procedurecode);

            procedurecode.ProgramServiceCategories = new List<ProgramServiceCategory>();
            PopulateAssignedProgramServiceCategoryData(procedurecode);

            procedurecode.StaffRequirements = new List<StaffRequirement>();
            PopulateAssignedStaffRequirementData(procedurecode);

            procedurecode.PlaceOfServices = new List<PlaceOfService>();
            PopulateAssignedPlaceOfServiceData(procedurecode);

            return View();
        }

        // POST: USCSForm/Create
        [HttpPost]


        //public ActionResult Create([Bind(Include = "Active,CPT_HCPCS_Code,Domain,PrimaryCategory,SecondaryCategory,TertiaryCategory,ProcedureCodeDescription,ServiceDescription,DocumentationRequirement1,ProcedureCodeNotes,Activity1,MinimumValue,MaximumValue,DurationMetric,Eff_Date,Thru_Date,Load_Date")]
        //                            ProcedureCode procedurecode, string[] selectedUsages, string[] selectedPopulations, string[] selectedUnits, string[] selectedModeOfDeliveries, string[] selectedProgramServiceCategories,
        //                            string[] selectedStaffRequirements, string[] selectedPlaceOfServices)
        public ActionResult Create([Bind(Include = "CPT_HCPCS_Code,ProcedureCodeDescription,ServiceDescription,ProcedureCodeNotes,Eff_Date,Thru_Date,Active,Load_Date")]
                                    ProcedureCode procedurecode, Category category, USCSFormVM viewmodel,string[] selectedUsages, string[] selectedPopulations, string[] selectedUnits, string[] selectedModeOfDeliveries, string[] selectedProgramServiceCategories,
                                    string[] selectedStaffRequirements, string[] selectedPlaceOfServices)
        {
            try
            {
                // TODO: Add insert logic here
                if (selectedUsages != null)
                {
                    procedurecode.Usages = new List<Usage>();
                    foreach (var usage in selectedUsages)
                    {
                        var usageToAdd = db.Usages.Find(int.Parse(usage));
                        procedurecode.Usages.Add(usageToAdd);
                    }
                }

                if (selectedPopulations != null)
                {
                    procedurecode.Populations = new List<Population>();
                    foreach (var population in selectedPopulations)
                    {
                        var populationToAdd = db.Populations.Find(int.Parse(population));
                        procedurecode.Populations.Add(populationToAdd);
                    }
                }

                if (selectedUnits != null)
                {
                    procedurecode.Units = new List<Unit>();
                    foreach (var unit in selectedUnits)
                    {
                        var unitToAdd = db.Units.Find(int.Parse(unit));
                        procedurecode.Units.Add(unitToAdd);
                    }
                }

                if (selectedModeOfDeliveries != null)
                {
                    procedurecode.ModeOfDeliveries = new List<ModeOfDelivery>();
                    foreach (var modeofdelivery in selectedModeOfDeliveries)
                    {
                        var modeofdeliveryToAdd = db.ModeOfDeliveries.Find(int.Parse(modeofdelivery));
                        procedurecode.ModeOfDeliveries.Add(modeofdeliveryToAdd);
                    }
                }

                if (selectedProgramServiceCategories != null)
                {
                    procedurecode.ProgramServiceCategories = new List<ProgramServiceCategory>();
                    foreach (var programservicecategory in selectedProgramServiceCategories)
                    {
                        var programservicecategoryToAdd = db.ProgramServiceCategories.Find(int.Parse(programservicecategory));
                        procedurecode.ProgramServiceCategories.Add(programservicecategoryToAdd);
                    }
                }

                if (selectedStaffRequirements != null)
                {
                    procedurecode.StaffRequirements = new List<StaffRequirement>();
                    foreach (var staffrequirement in selectedStaffRequirements)
                    {
                        var staffrequirementToAdd = db.StaffRequirements.Find(int.Parse(staffrequirement));
                        procedurecode.StaffRequirements.Add(staffrequirementToAdd);
                    }
                }

                if (selectedPlaceOfServices != null)
                {
                    procedurecode.PlaceOfServices = new List<PlaceOfService>();
                    foreach (var placeofservice in selectedPlaceOfServices)
                    {
                        var placeofserviceToAdd = db.PlaceOfServices.Find(int.Parse(placeofservice));
                        procedurecode.PlaceOfServices.Add(placeofserviceToAdd);
                    }
                }

                if (ModelState.IsValid)
                {

                    //int LastCategoryID = db.Categories.Max(m => m.CategoryID);
                    //int LastProcedureCodeID= db.ProcedureCodes.Max(m => m.ProcedureCodeID) + 1;
                    //int CategoryPrimaryKey = category.CategoryID;
                    //int ProcedureCodePrimaryKey = procedurecode.ProcedureCodeID;

                    procedurecode.Categories.Add(category);
                    //category.ProcedureCodes.Add(procedurecode);

                    db.Categories.Add(category);
                    db.SaveChanges();

                    db.ProcedureCodes.Add(procedurecode);
                    db.SaveChanges();

                    //return RedirectToAction("Index");
                    return Redirect("/ProcedureCodes/Index");


                }

                PopulateAssignedUsageData(procedurecode);
                PopulateAssignedPopulationData(procedurecode);
                PopulateAssignedUnitData(procedurecode);
                PopulateAssignedModeOfDeliveryData(procedurecode);
                PopulateAssignedProgramServiceCategoryData(procedurecode);
                PopulateAssignedStaffRequirementData(procedurecode);
                PopulateAssignedPlaceOfServiceData(procedurecode);

                //return View(procedurecode);
                return View();

            }
            catch
            {
                return View();
            }
        }

        private void PopulateAssignedUsageData(ProcedureCode procedurecode)
        {
            var allUsages = db.Usages;
            var procedurecodeUsages = new HashSet<int>(procedurecode.Usages.Select(c => c.UsageID));
            var viewModel = new List<AssignedUsageData>();
            foreach (var usage in allUsages)
            {
                viewModel.Add(new AssignedUsageData
                {
                    UsageID = usage.UsageID,
                    Usage1 = usage.Usage1,
                    Assigned = procedurecodeUsages.Contains(usage.UsageID)
                });
            }
            ViewBag.Usages = viewModel;
        }

        private void PopulateAssignedPopulationData(ProcedureCode procedurecode)
        {
            var allPopulations = db.Populations;
            var procedurecodePopulations = new HashSet<int>(procedurecode.Populations.Select(c => c.PopulationID));
            var viewModel = new List<AssignedPopulationData>();
            foreach (var population in allPopulations)
            {
                viewModel.Add(new AssignedPopulationData
                {
                    PopulationID = population.PopulationID,
                    Population1 = population.Population1,
                    AgeRange = population.AgeRange,
                    Assigned = procedurecodePopulations.Contains(population.PopulationID)
                });
            }
            ViewBag.Populations = viewModel;
        }

        private void PopulateAssignedUnitData(ProcedureCode procedurecode)
        {
            var allUnits = db.Units;
            var procedurecodeUnits = new HashSet<int>(procedurecode.Units.Select(c => c.UnitID));
            var viewModel = new List<AssignedUnitData>();
            foreach (var unit in allUnits)
            {
                viewModel.Add(new AssignedUnitData
                {
                    UnitID = unit.UnitID,
                    UnitMetric = unit.UnitMetric,
                    UnitValue = unit.UnitValue,
                    Assigned = procedurecodeUnits.Contains(unit.UnitID)
                });
            }
            ViewBag.Units = viewModel;
        }
        private void PopulateAssignedModeOfDeliveryData(ProcedureCode procedurecode)
        {
            var allModeOfDeliveries = db.ModeOfDeliveries;
            var procedurecodeModeOfDeliveries = new HashSet<int>(procedurecode.ModeOfDeliveries.Select(c => c.ModeOfDeliveryID));
            var viewModel = new List<AssignedModeOfDeliveryData>();
            foreach (var modeofdelivery in allModeOfDeliveries)
            {
                viewModel.Add(new AssignedModeOfDeliveryData
                {
                    ModeOfDeliveryID = modeofdelivery.ModeOfDeliveryID,
                    ModeOfDelivery1 = modeofdelivery.ModeOfDelivery1,
                    Assigned = procedurecodeModeOfDeliveries.Contains(modeofdelivery.ModeOfDeliveryID)
                });
            }
            ViewBag.ModeOfDeliveries = viewModel;
        }

        private void PopulateAssignedProgramServiceCategoryData(ProcedureCode procedurecode)
        {
            var allProgramServiceCategories = db.ProgramServiceCategories;
            var procedurecodeProgramServiceCategories = new HashSet<int>(procedurecode.ProgramServiceCategories.Select(c => c.ProgramServiceCategoryID));
            var viewModel = new List<AssignedProgramServiceCategoryData>();
            foreach (var programservicecategory in allProgramServiceCategories)
            {
                viewModel.Add(new AssignedProgramServiceCategoryData
                {
                    ProgramServiceCategoryID = programservicecategory.ProgramServiceCategoryID,
                    ProgramServiceCategory1 = programservicecategory.ProgramServiceCategory1,
                    Assigned = procedurecodeProgramServiceCategories.Contains(programservicecategory.ProgramServiceCategoryID)
                });
            }
            ViewBag.ProgramServiceCategories = viewModel;
        }

        private void PopulateAssignedStaffRequirementData(ProcedureCode procedurecode)
        {
            var allStaffRequirements = db.StaffRequirements;
            var procedurecodeStaffRequirements = new HashSet<int>(procedurecode.StaffRequirements.Select(c => c.StaffRequirementID));
            var viewModel = new List<AssignedStaffRequirementData>();
            foreach (var staffrequirement in allStaffRequirements)
            {
                viewModel.Add(new AssignedStaffRequirementData
                {
                    StaffRequirementID = staffrequirement.StaffRequirementID,
                    StaffLicensure = staffrequirement.StaffLicensure,
                    Assigned = procedurecodeStaffRequirements.Contains(staffrequirement.StaffRequirementID)
                });
            }
            ViewBag.StaffRequirements = viewModel;
        }

        private void PopulateAssignedPlaceOfServiceData(ProcedureCode procedurecode)
        {
            var allPlaceOfServices = db.PlaceOfServices;
            var procedurecodePlaceOfServices = new HashSet<int>(procedurecode.PlaceOfServices.Select(c => c.PlaceOfServiceID));
            var viewModel = new List<AssignedPlaceOfServiceData>();
            foreach (var placeofservice in allPlaceOfServices)
            {
                viewModel.Add(new AssignedPlaceOfServiceData
                {
                    PlaceOfServiceID = placeofservice.PlaceOfServiceID,
                    POSName = placeofservice.POSName,
                    POSCode = placeofservice.POSCode,
                    Assigned = procedurecodePlaceOfServices.Contains(placeofservice.PlaceOfServiceID)
                });
            }
            ViewBag.PlaceOfServices = viewModel;
        }


        // GET: USCSForm/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: USCSForm/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: USCSForm/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: USCSForm/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult GetDomainName(string q)
        {
            var DomainNames = (from p in db.Categories where p.Domain.Contains(q) select p.Domain).Distinct().Take(10);

            string content = string.Join<string>("\n", DomainNames);
            return Content(content);
        }
        public ActionResult GetPrimaryCategoryName(string q)
        {
            var PrimaryCategoryNames = (from o in db.Categories where o.PrimaryCategory.Contains(q) select o.PrimaryCategory).Distinct().Take(10);

            string content2 = string.Join<string>("\n", PrimaryCategoryNames);
            return Content(content2);
        }

        public ActionResult GetSecondaryCategoryName(string q)
        {
            var SecondaryCategoryNames = (from s in db.Categories where s.SecondaryCategory.Contains(q) select s.SecondaryCategory).Distinct().Take(10);

            string contentsc = string.Join<string>("\n", SecondaryCategoryNames);
            return Content(contentsc);
        }

        public ActionResult GetTertiaryCategoryName(string q)
        {
            var TertiaryCategoryNames = (from t in db.Categories where t.TertiaryCategory.Contains(q) select t.TertiaryCategory).Distinct().Take(10);

            string contenttc = string.Join<string>("\n", TertiaryCategoryNames);
            return Content(contenttc);
        }
        public ActionResult GetCPT_HCPCS_Code(string q)
        {
            var CPT_HCPCS_Codes = (from t in db.ProcedureCodes where t.CPT_HCPCS_Code.Contains(q) select t.CPT_HCPCS_Code).Distinct().Take(10);

            string contenttc = string.Join<string>("\n", CPT_HCPCS_Codes);
            return Content(contenttc);
        }


        public ActionResult GetDurationMinimumValue(string q)
        {
            var DurationMinimumValue = (from t in db.Durations where t.MinimumValue.Contains(q) select t.MinimumValue).Distinct().Take(10);

            string contenttc = string.Join<string>("\n", DurationMinimumValue);
            return Content(contenttc);
        }
        public ActionResult GetDurationMaximumValue(string q)
        {
            var DurationMaximumValue = (from t in db.Durations where t.MaximumValue.Contains(q) select t.MaximumValue).Distinct().Take(10);

            string contenttc = string.Join<string>("\n", DurationMaximumValue);
            return Content(contenttc);
        }
        public ActionResult GetDurationMetric(string q)
        {
            var DurationMetric = (from t in db.Durations where t.DurationMetric.Contains(q) select t.DurationMetric).Distinct().Take(10);

            string contenttc = string.Join<string>("\n", DurationMetric);
            return Content(contenttc);
        }
    }
}
