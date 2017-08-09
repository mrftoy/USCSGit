using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using USCS.Models;
using USCS.ViewModels;

namespace USCS.Controllers
{
    public class ProcedureCodesController : Controller
    {
        private USCSModel db = new USCSModel();

        // GET: ProcedureCodes
        public ActionResult Index()
        {
            return View(db.ProcedureCodes.ToList());
        }

        // GET: ProcedureCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcedureCode procedureCode = db.ProcedureCodes.Find(id);
            if (procedureCode == null)
            {
                return HttpNotFound();
            }
            return View(procedureCode);
        }

        // GET: ProcedureCodes/Create
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

        // POST: ProcedureCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "CPT_HCPCS_Code,ProcedureCodeDescription,ServiceDescription,ProcedureCodeNotes,Eff_Date,Thru_Date,Active,Load_Date")]
                                    ProcedureCode procedurecode, Category category, DocumentationRequirement documentationrequirement, Activity activity, Duration duration, USCSFormVM viewmodel,
                                    string[] selectedUsages, string[] selectedPopulations, string[] selectedUnits, string[] selectedModeOfDeliveries, string[] selectedProgramServiceCategories,
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

                    procedurecode.Categories.Add(category);
                    procedurecode.DocumentationRequirements.Add(documentationrequirement);
                    procedurecode.Activities.Add(activity);
                    procedurecode.Durations.Add(duration);
                    //procedurecode.Modifiers.Add(modifier);

                    db.Categories.Add(category);
                    db.DocumentationRequirements.Add(documentationrequirement);
                    db.Activities.Add(activity);
                    db.Durations.Add(duration);
                    db.SaveChanges();

                    db.ProcedureCodes.Add(procedurecode);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                    //return Redirect("/ProcedureCodes/Index");
                }

                PopulateCategoryData(procedurecode);
                PopulateDocumentationRequirementData(procedurecode);
                PopulateActivityData(procedurecode);
                PopulateDurationData(procedurecode);
                PopulateAssignedUsageData(procedurecode);
                PopulateAssignedPopulationData(procedurecode);
                PopulateAssignedUnitData(procedurecode);
                PopulateAssignedModeOfDeliveryData(procedurecode);
                PopulateAssignedProgramServiceCategoryData(procedurecode);
                PopulateAssignedStaffRequirementData(procedurecode);
                PopulateAssignedPlaceOfServiceData(procedurecode);

                return View(procedurecode);
            }
            catch
            {
                return View();
            }
        }

        private void PopulateCategoryData(ProcedureCode procedurecode)
        {
            var allCategories = db.Categories;
            var procedurecodeCategories = new HashSet<int>(procedurecode.Categories.Select(c => c.CategoryID));
            var viewModel = new List<CategoryData>();
            foreach (var category in allCategories)
            {
                viewModel.Add(new CategoryData
                {
                    CategoryID = category.CategoryID,
                    Domain = category.Domain,
                    PrimaryCategory = category.PrimaryCategory,
                    SecondaryCategory = category.SecondaryCategory,
                    TertiaryCategory = category.TertiaryCategory
                });
            }
            ViewBag.Categories = viewModel;
        }

        private void PopulateDocumentationRequirementData(ProcedureCode procedurecode)
        {
            var allDocumentationRequirements = db.DocumentationRequirements;
            var procedurecodeDocumentationRequirements = new HashSet<int>(procedurecode.DocumentationRequirements.Select(c => c.DocumentationRequirementID));
            var viewModel = new List<DocumentationRequirementData>();
            foreach (var documentationrequirement in allDocumentationRequirements)
            {
                viewModel.Add(new DocumentationRequirementData
                {
                    DocumentationRequirementID = documentationrequirement.DocumentationRequirementID,
                    DocumentationRequirement1 = documentationrequirement.DocumentationRequirement1
                });
            }
            ViewBag.DocumentationRequirements = viewModel;
        }
        private void PopulateActivityData(ProcedureCode procedurecode)
        {
            var allActivities = db.Activities;
            var procedurecodeActivities = new HashSet<int>(procedurecode.Activities.Select(c => c.ActivityID));
            var viewModel = new List<ActivityData>();
            foreach (var activity in allActivities)
            {
                viewModel.Add(new ActivityData
                {
                    ActivityID = activity.ActivityID,
                    Activity1 = activity.Activity1
                });
            }
            ViewBag.Activities = viewModel;
        }
        private void PopulateDurationData(ProcedureCode procedurecode)
        {
            var allDurations = db.Durations;
            var procedurecodeDurations = new HashSet<int>(procedurecode.Durations.Select(c => c.DurationID));
            var viewModel = new List<DurationData>();
            foreach (var duration in allDurations)
            {
                viewModel.Add(new DurationData
                {
                    DurationID = duration.DurationID,
                    MinimumValue = duration.MinimumValue,
                    MaximumValue = duration.MaximumValue,
                    DurationMetric = duration.DurationMetric
                });
            }
            ViewBag.Activities = viewModel;
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

        // GET: ProcedureCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProcedureCode procedurecode = db.ProcedureCodes
                .Include(i => i.Activities)
                .Include(i => i.Categories)
                .Include(i => i.DocumentationRequirements)
                .Include(i => i.Durations)
                .Include(i => i.ModeOfDeliveries)
                .Include(i => i.Modifiers)
                .Include(i => i.PlaceOfServices)
                .Include(i => i.Populations)
                .Include(i => i.StaffRequirements)
                .Include(i => i.Units)
                .Include(i => i.Usages)
                .Include(i => i.ProgramServiceCategories)
                .Where(i => i.ProcedureCodeID == id)
                .Single();
            PopulateCategoryData(procedurecode);
            PopulateDurationData(procedurecode);
            PopulateAssignedUsageData(procedurecode);
            PopulateAssignedPopulationData(procedurecode);
            PopulateAssignedUnitData(procedurecode);
            PopulateAssignedModeOfDeliveryData(procedurecode);
            PopulateAssignedProgramServiceCategoryData(procedurecode);
            PopulateAssignedStaffRequirementData(procedurecode);
            PopulateAssignedPlaceOfServiceData(procedurecode);

            if (procedurecode == null)
            {
                return HttpNotFound();
            }

            return View(procedurecode);

        }

        // POST: ProcedureCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedActivities, string[] selectedCategories, string[] selectedDocumentationRequirements, string[] selectedDurations,
                                 string[] selectedModeOfDeliveries, string[] selectedModifiers, string[] selectedPlaceOfServices, string[] selectedPopulations,
                                 string[] selectedStaffRequirements, string[] selectedUnits, string[] selectedUsages, string[] selectedProgramServiceCategories)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var procedurecodeToUpdate = db.ProcedureCodes
                .Include(i => i.Categories)
                .Include(i => i.Modifiers)
                .Include(i => i.Usages)
                .Include(i => i.Activities)
                .Include(i => i.Populations)
                .Include(i => i.Durations)
                .Include(i => i.Units)
                .Include(i => i.ModeOfDeliveries)
                .Include(i => i.ProgramServiceCategories)
                .Include(i => i.StaffRequirements)
                .Include(i => i.PlaceOfServices)
                .Where(i => i.ProcedureCodeID == id)
                .Single();

            if (TryUpdateModel(procedurecodeToUpdate, "",
               new string[] { "CPT_HCPCS_Code", "ProcedureCodeDescription", "ServiceDescription", "ProcedureCodeNotes", "Eff_Date",
                              "Thru_Date", "Active", "Load_Date" }))
            {
                try
                {

                    UpdateProcedureCodeUsages(selectedUsages, procedurecodeToUpdate);
                    UpdateProcedureCodePopulations(selectedPopulations, procedurecodeToUpdate);
                    UpdateProcedureCodeUnits(selectedUnits, procedurecodeToUpdate);
                    UpdateProcedureCodeModeOfDeliveries(selectedModeOfDeliveries, procedurecodeToUpdate);
                    UpdateProcedureCodeProgramServiceCategories(selectedProgramServiceCategories, procedurecodeToUpdate);
                    UpdateProcedureCodeStaffRequirements(selectedStaffRequirements, procedurecodeToUpdate);
                    UpdateProcedureCodePlaceOfServices(selectedPlaceOfServices, procedurecodeToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedUsageData(procedurecodeToUpdate);
            PopulateAssignedPopulationData(procedurecodeToUpdate);
            PopulateAssignedUnitData(procedurecodeToUpdate);
            PopulateAssignedModeOfDeliveryData(procedurecodeToUpdate);
            PopulateAssignedProgramServiceCategoryData(procedurecodeToUpdate);
            PopulateAssignedStaffRequirementData(procedurecodeToUpdate);
            PopulateAssignedPlaceOfServiceData(procedurecodeToUpdate);
            return View(procedurecodeToUpdate);

        }

        private void UpdateProcedureCodeUsages(string[] selectedUsages, ProcedureCode procedurecodeToUpdate)
        {
            if (selectedUsages == null)
            {
                procedurecodeToUpdate.Usages = new List<Usage>();
                return;
            }

            var selectedUsagesHS = new HashSet<string>(selectedUsages);
            var procedurecodeUsages = new HashSet<int>
                (procedurecodeToUpdate.Usages.Select(c => c.UsageID));
            foreach (var usage in db.Usages)
            {
                if (selectedUsagesHS.Contains(usage.UsageID.ToString()))
                {
                    if (!procedurecodeUsages.Contains(usage.UsageID))
                    {
                        procedurecodeToUpdate.Usages.Add(usage);
                    }
                }
                else
                {
                    if (procedurecodeUsages.Contains(usage.UsageID))
                    {
                        procedurecodeToUpdate.Usages.Remove(usage);
                    }
                }
            }
        }

        private void UpdateProcedureCodePopulations(string[] selectedPopulations, ProcedureCode procedurecodeToUpdate)
        {
            if (selectedPopulations == null)
            {
                procedurecodeToUpdate.Populations = new List<Population>();
                return;
            }

            var selectedPopulationsHS = new HashSet<string>(selectedPopulations);
            var procedurecodePopulations = new HashSet<int>
                (procedurecodeToUpdate.Populations.Select(c => c.PopulationID));
            foreach (var population in db.Populations)
            {
                if (selectedPopulationsHS.Contains(population.PopulationID.ToString()))
                {
                    if (!procedurecodePopulations.Contains(population.PopulationID))
                    {
                        procedurecodeToUpdate.Populations.Add(population);
                    }
                }
                else
                {
                    if (procedurecodePopulations.Contains(population.PopulationID))
                    {
                        procedurecodeToUpdate.Populations.Remove(population);
                    }
                }
            }
        }

        private void UpdateProcedureCodeUnits(string[] selectedUnits, ProcedureCode procedurecodeToUpdate)
        {
            if (selectedUnits == null)
            {
                procedurecodeToUpdate.Units = new List<Unit>();
                return;
            }

            var selectedUnitsHS = new HashSet<string>(selectedUnits);
            var procedurecodeUnits = new HashSet<int>
                (procedurecodeToUpdate.Units.Select(c => c.UnitID));
            foreach (var unit in db.Units)
            {
                if (selectedUnitsHS.Contains(unit.UnitID.ToString()))
                {
                    if (!procedurecodeUnits.Contains(unit.UnitID))
                    {
                        procedurecodeToUpdate.Units.Add(unit);
                    }
                }
                else
                {
                    if (procedurecodeUnits.Contains(unit.UnitID))
                    {
                        procedurecodeToUpdate.Units.Remove(unit);
                    }
                }
            }
        }

        private void UpdateProcedureCodeModeOfDeliveries(string[] selectedModeOfDeliveries, ProcedureCode procedurecodeToUpdate)
        {
            if (selectedModeOfDeliveries == null)
            {
                procedurecodeToUpdate.ModeOfDeliveries = new List<ModeOfDelivery>();
                return;
            }

            var selectedModeOfDeliveriesHS = new HashSet<string>(selectedModeOfDeliveries);
            var procedurecodeModeOfDeliveries = new HashSet<int>
                (procedurecodeToUpdate.ModeOfDeliveries.Select(c => c.ModeOfDeliveryID));
            foreach (var modeofdelivery in db.ModeOfDeliveries)
            {
                if (selectedModeOfDeliveriesHS.Contains(modeofdelivery.ModeOfDeliveryID.ToString()))
                {
                    if (!procedurecodeModeOfDeliveries.Contains(modeofdelivery.ModeOfDeliveryID))
                    {
                        procedurecodeToUpdate.ModeOfDeliveries.Add(modeofdelivery);
                    }
                }
                else
                {
                    if (procedurecodeModeOfDeliveries.Contains(modeofdelivery.ModeOfDeliveryID))
                    {
                        procedurecodeToUpdate.ModeOfDeliveries.Remove(modeofdelivery);
                    }
                }
            }
        }

        private void UpdateProcedureCodeProgramServiceCategories(string[] selectedProgramServiceCategories, ProcedureCode procedurecodeToUpdate)
        {
            if (selectedProgramServiceCategories == null)
            {
                procedurecodeToUpdate.ProgramServiceCategories = new List<ProgramServiceCategory>();
                return;
            }

            var selectedProgramServiceCategoriesHS = new HashSet<string>(selectedProgramServiceCategories);
            var procedurecodeProgramServiceCategories = new HashSet<int>
                (procedurecodeToUpdate.ProgramServiceCategories.Select(c => c.ProgramServiceCategoryID));
            foreach (var programservicecategory in db.ProgramServiceCategories)
            {
                if (selectedProgramServiceCategoriesHS.Contains(programservicecategory.ProgramServiceCategoryID.ToString()))
                {
                    if (!procedurecodeProgramServiceCategories.Contains(programservicecategory.ProgramServiceCategoryID))
                    {
                        procedurecodeToUpdate.ProgramServiceCategories.Add(programservicecategory);
                    }
                }
                else
                {
                    if (procedurecodeProgramServiceCategories.Contains(programservicecategory.ProgramServiceCategoryID))
                    {
                        procedurecodeToUpdate.ProgramServiceCategories.Remove(programservicecategory);
                    }
                }
            }
        }

        private void UpdateProcedureCodeStaffRequirements(string[] selectedStaffRequirements, ProcedureCode procedurecodeToUpdate)
        {
            if (selectedStaffRequirements == null)
            {
                procedurecodeToUpdate.StaffRequirements = new List<StaffRequirement>();
                return;
            }

            var selectedStaffRequirementsHS = new HashSet<string>(selectedStaffRequirements);
            var procedurecodeStaffRequirements = new HashSet<int>
                (procedurecodeToUpdate.StaffRequirements.Select(c => c.StaffRequirementID));
            foreach (var staffrequirement in db.StaffRequirements)
            {
                if (selectedStaffRequirementsHS.Contains(staffrequirement.StaffRequirementID.ToString()))
                {
                    if (!procedurecodeStaffRequirements.Contains(staffrequirement.StaffRequirementID))
                    {
                        procedurecodeToUpdate.StaffRequirements.Add(staffrequirement);
                    }
                }
                else
                {
                    if (procedurecodeStaffRequirements.Contains(staffrequirement.StaffRequirementID))
                    {
                        procedurecodeToUpdate.StaffRequirements.Remove(staffrequirement);
                    }
                }
            }
        }

        private void UpdateProcedureCodePlaceOfServices(string[] selectedPlaceOfServices, ProcedureCode procedurecodeToUpdate)
        {
            if (selectedPlaceOfServices == null)
            {
                procedurecodeToUpdate.PlaceOfServices = new List<PlaceOfService>();
                return;
            }

            var selectedPlaceOfServicesHS = new HashSet<string>(selectedPlaceOfServices);
            var procedurecodePlaceOfServices = new HashSet<int>
                (procedurecodeToUpdate.PlaceOfServices.Select(c => c.PlaceOfServiceID));
            foreach (var placeofservice in db.PlaceOfServices)
            {
                if (selectedPlaceOfServicesHS.Contains(placeofservice.PlaceOfServiceID.ToString()))
                {
                    if (!procedurecodePlaceOfServices.Contains(placeofservice.PlaceOfServiceID))
                    {
                        procedurecodeToUpdate.PlaceOfServices.Add(placeofservice);
                    }
                }
                else
                {
                    if (procedurecodePlaceOfServices.Contains(placeofservice.PlaceOfServiceID))
                    {
                        procedurecodeToUpdate.PlaceOfServices.Remove(placeofservice);
                    }
                }
            }
        }

        // GET: ProcedureCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProcedureCode procedureCode = db.ProcedureCodes.Find(id);
            if (procedureCode == null)
            {
                return HttpNotFound();
            }
            return View(procedureCode);
        }

        // POST: ProcedureCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            ProcedureCode procedureCode = db.ProcedureCodes.Find(id);
            db.ProcedureCodes.Remove(procedureCode);
            var IsActive = "N";
            procedureCode.Active = IsActive;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
        //public ActionResult GetModifier1(string q)
        //{
        //    var Modifier1s = (from t in db.Modifiers where t.Modifier1.Contains(q) select t.Modifier1).Distinct().Take(10);

        //    string contenttc = string.Join<string>("\n", Modifier1s);
        //    return Content(contenttc);
        //}

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
