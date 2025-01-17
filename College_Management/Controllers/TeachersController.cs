﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using College_Management.DAL;
using College_Management.Models;

namespace College_Management.Controllers
{
    public class TeachersController : Controller
    {
        private CollegeContext db = new CollegeContext();

        // GET: Teachers
        public async Task<ActionResult> Index()
        {
            return View(await db.Teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "Title");
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        public async Task<JsonResult> Create([Bind(Include = "Name,Birthday,Salary")] Teacher teacher)
        {
            db.Teachers.Add(teacher);
            await db.SaveChangesAsync();
            return Json(db.Teachers.ToList().LastOrDefault<Teacher>());
        }

        // GET: Teachers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            //ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "Title", teacher.SubjectId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<JsonResult> Edit([Bind(Include = "Id,Name,Birthday,Salary")] Teacher teacher)
        {
            try
            {
                db.Entry(teacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Json(new { result = "ok", status = 200 });
            }
            catch (Exception ex)
            {
                return Json(new { result = "error", status = 200, message = ex.InnerException.InnerException.Message.ToString() });
            }
        }

        // GET: Teachers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception("Parameter id should not be null!");
                }
                Teacher teacher = await db.Teachers.FindAsync(id);
                db.Teachers.Remove(teacher);
                await db.SaveChangesAsync();
                return Json(new { result = "ok", status = 200 });
            }
            catch (Exception ex)
            {
                return Json(new { result = "error", status = 200, message = ex.Message.ToString() });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public JsonResult GetData()
        {
            var teachers = db.Teachers.ToList();
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

    }
}
