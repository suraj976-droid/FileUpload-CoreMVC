using CodeFirstApproach.Data;
using CodeFirstApproach.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstApproach.Controllers
{
    public class EmpController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;

        public EmpController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {
            var data = db.emps.ToList();
            return View(data);
        }

        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmp(EmpView e)
        {
            if(ModelState.IsValid)
            {
                string path = env.WebRootPath;
                string filepath = "Content/Images/" + e.eimg.FileName;
                string fullpath = Path.Combine(path, filepath);
                UploadFile(e.eimg,fullpath);

                var em = new Emp()
                {
                    empName = e.empName,
                    empEmail = e.empEmail,
                    empSalary = e.empSalary,
                    eimg = filepath
                };

                db.emps.Add(em);
                db.SaveChanges();
                TempData["success"] = "Emp Added Successfully!!"; 
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public void UploadFile (IFormFile file,string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }

        public IActionResult DeleteEmp(int id)
        {
            //var data = db.emps.Where(x => x.empId.Equals(id)).SingleOrDefault();
            var data = db.emps.Find(id);
            if(data != null)
            {
                db.emps.Remove(data);
                db.SaveChanges();
                TempData["error"] = "Emp Deleted Successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult EditEmp(int id)
        {
            var data = db.emps.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditEmp(Emp e,IFormFile eimg)
        {
            
                Emp em;

                if (eimg == null)
                {
                    var img = db.emps.Where(x => x.empId.Equals(e.empId)).Select(x=> x.eimg).SingleOrDefault();
                    em = new Emp()
                    {
                        empId = e.empId,
                        empName = e.empName,
                        empEmail = e.empEmail,
                        empSalary = e.empSalary,
                        eimg = img
                    };
                }

                else
                {
                    string path = env.WebRootPath;
                    string filepath = "Content/Images/" + eimg.FileName;
                    string fullpath = Path.Combine(path, filepath);
                    UploadFile(eimg, fullpath);

                    em = new Emp()
                    {
                        empId = e.empId,
                        empName = e.empName,
                        empEmail = e.empEmail,
                        empSalary = e.empSalary,
                        eimg = filepath
                    };

                    
                }
            db.emps.Update(em);
            db.SaveChanges();
            TempData["upd"] = "Emp Updated Successfully!!";
            return RedirectToAction("Index");

           
            
        }
    }
}
