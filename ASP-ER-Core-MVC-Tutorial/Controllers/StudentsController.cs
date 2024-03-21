using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_ER_Core_MVC_Tutorial.Data;
using ASP_ER_Core_MVC_Tutorial.Models;

namespace ASP_ER_Core_MVC_Tutorial.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return _context.Students != null
                  ? View(await _context.Students.ToListAsync())
                  : Problem("Entity set 'SchoolContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            // var student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);
            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            // Include and ThenInclude are used to specify related data to be included in the query results.
            // Without these, Enrollments and Course would be null in the view model.
            
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Bind attribute is used to specify which properties you want to include in the model binding process.
        // Here we tell the model binder to bind everything except the ID.
        // This prevents overposting attacks in which a malicious user includes additional form fields in the request.
        public async Task<IActionResult> Create([Bind("LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                // Logging stuff:
                // https://learn.microsoft.com/en-us/aspnet/aspnet/overview/developing-apps-with-windows-azure/building-real-world-cloud-apps-with-windows-azure/monitoring-and-telemetry
                
                ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists " +
                                         "see your system administrator.");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            
            if (student == null)
            {
                return NotFound();
            }
            
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentToUpdate = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            // TryUpdateModelAsync uses the data in the posted form to update the specified entity.
            // That's why we don't need to pass the entity to the method.
            var canUpdate = await TryUpdateModelAsync<Student>(
                studentToUpdate,
                "",
                s => s.FirstMidName,
                s => s.LastName,
                s => s.EnrollmentDate);
            
            // The above method requires a database read to get the entity to update.
            // Alternative stuff here:
            // https://learn.microsoft.com/en-gb/aspnet/core/data/ef-mvc/crud?view=aspnetcore-8.0#alternative-httppost-edit-code-create-and-attach

            if (canUpdate)
            {
                try
                {
                    // the SaveChanges method makes the Entity Framework create SQL statements to update the database.
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    // Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists, " +
                                             "see your system administrator.");
                }
            }
            
            return View(studentToUpdate);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again, and if the problem persists " +
                                           "see your system administrator.";
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Alternative "create-and-attach" method here:
            // https://learn.microsoft.com/en-gb/aspnet/core/data/ef-mvc/crud?view=aspnetcore-8.0#the-create-and-attach-approach-to-httppost-delete
            
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                // Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
