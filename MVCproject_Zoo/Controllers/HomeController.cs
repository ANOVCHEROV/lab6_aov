using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCproject_Zoo.DAO;
using MVCproject_Zoo.Models;

namespace MVCproject_Zoo.Controllers
{
    public class HomeController : Controller
    {
        RecordsDAO recordsDAO = new RecordsDAO();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View(recordsDAO.GetAllRecords());
        }
        public ActionResult Create()
        {
            return View();
        }
        //
        // POST: /Home/Create
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "ID")] Animals records)
        {
            try
            {
                records.AddingDate = DateTime.Now;
                if (recordsDAO.AddRecord(records))
                    return RedirectToAction("Index");
                else
                    return View("Create");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", new { ErrorText = ex.Message });
            }
        }

        // GET: Home/Details/5
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Details(int id)
        {
            RecordsDAO r = new RecordsDAO();
            return View(r.getById(id));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Details()
        {
            return View();
        }



        // GET: Home/Edit/5
        [HttpGet]

        public ActionResult Edit(int id)
        {
            RecordsDAO r = new RecordsDAO();
            return View(r.getById(id));
        }

        [HttpPost]

        public ActionResult Edit(Animals record)
        {
            if (ModelState.IsValid)
            {
                record.AddingDate = DateTime.Now;
                recordsDAO.EditRecord(record);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit");
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            RecordsDAO r = new RecordsDAO();
            return View(r.getById(id));
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                RecordsDAO r = new RecordsDAO();
                r.DeleteRecord(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

    }
}
