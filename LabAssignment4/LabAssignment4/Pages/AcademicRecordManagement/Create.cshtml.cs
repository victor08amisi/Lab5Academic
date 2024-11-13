using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LabAssignment4.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace LabAssignment4.Pages.AcademicRecordManagement
{
    public class CreateModel : PageModel
    {
        private readonly LabAssignment4.DataAccess.StudentrecordContext _context;

        public CreateModel(LabAssignment4.DataAccess.StudentrecordContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string ExistError { get; set; }

        public IActionResult OnGet()
        {
            // Create a SelectList for courses showing both Course Code and Title
            ViewData["CourseCode"] = new SelectList(
                _context.Courses.Select(c => new
                {
                    Code = c.Code,
                    Title = c.Title,
                    Display = $"{c.Code} - {c.Title}" // Combine Code and Title for display
                }).ToList(),
                "Code", "Display");  // Use Code as value and combined Display for text

            // Create a SelectList for students showing both Student Id and Name
            ViewData["StudentId"] = new SelectList(
                _context.Students.Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    Display = $"{s.Id} - {s.Name}" // Combine Id and Name for display
                }).ToList(),
                "Id", "Display");  // Use Id as value and combined Display for text

            return Page();
        }

        [BindProperty]
        public AcademicRecord AcademicRecord { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine(AcademicRecord.StudentId);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the academic record already exists for the given student and course
            var existingRecord = await _context.AcademicRecords
                .FirstOrDefaultAsync(ar => ar.StudentId == AcademicRecord.StudentId && ar.CourseCode == AcademicRecord.CourseCode);
            Console.WriteLine(existingRecord.StudentId);
            if (existingRecord != null)
            {
                // If the record exists, add an error to the ModelState
                ModelState.AddModelError(string.Empty, "An academic record for this student and course already exists.");
                return Page();  // Return to the page with the error message
            }

            // If the record doesn't exist, add the new academic record
            _context.AcademicRecords.Add(AcademicRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");  // Redirect to the index page after successful submission
        }


    }
}
