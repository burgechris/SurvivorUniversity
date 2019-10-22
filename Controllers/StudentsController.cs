using Microsoft.AspNetCore.Mvc;
using University.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace University.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly UniversityContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentsController(UserManager<ApplicationUser> userManager, UniversityContext db)
        {
            _userManager = userManager;    
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            var userItems = _db.Students.Where(entry => entry.User.Id == currentUser.Id);
            return View(userItems);
        }

        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Student student, int CourseId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            student.User = currentUser;
            _db.Students.Add(student);
            if (CourseId != 0)
            {
                _db.Enrollment.Add(new Enrollment() { CourseId = CourseId, StudentId = student.StudentId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var thisStudent = _db.Students
              .Include(student => student.Courses)
              .ThenInclude(join => join.Course)
              .FirstOrDefault(student => student.StudentId == id);
            return View(thisStudent);
        }

        public ActionResult EnrollCourse(int id)
        {
            var thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
            ViewBag.CourseId = new SelectList(_db.Courses, "CategoryId", "Name");
            return View(thisStudent);
        }

        [HttpPost]
        public ActionResult EnrollCourse(Student student, int CourseId)
        {
            if (CourseId != 0)
            {
                _db.Enrollment.Add(new Enrollment() { CourseId = CourseId, StudentId = student.StudentId });
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
            return View(thisStudent);
        }

        [HttpPost]
        public ActionResult Edit(Student student, int CourseId)
        {
            if (CourseId != 0)
            {
                _db.Enrollment.Add(new Enrollment() { CourseId = CourseId, StudentId = student.StudentId });
            }
            _db.Entry(student).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
            return View(thisStudent);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisStudent = _db.Students.FirstOrDefault(students => students.StudentId == id);
            _db.Students.Remove(thisStudent);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteCourse(int joinId)
        {
            var joinEntry = _db.Enrollment.FirstOrDefault(entry => entry.EnrollmentId == joinId);
            _db.Enrollment.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}