using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LabAssignment4.DataAccess;

namespace LabAssignment4.Pages.StudentManagement
{
    public class CreateModel : PageModel
    {
        private readonly LabAssignment4.DataAccess.StudentrecordContext _context;

        public CreateModel(LabAssignment4.DataAccess.StudentrecordContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the student ID already exists
            var existingStudent = _context.Students
                .FirstOrDefault(s => s.Id == Student.Id);

            if (existingStudent != null)
            {
                // Add an error to the ModelState
                ModelState.AddModelError("Student.Id", "A student with this ID already exists.");
                return Page();
            }

            // Add the new student
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
