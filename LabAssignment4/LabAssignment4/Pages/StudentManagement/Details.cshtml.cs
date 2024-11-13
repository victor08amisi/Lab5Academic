using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabAssignment4.DataAccess;

namespace LabAssignment4.Pages.StudentManagement
{
    public class DetailsModel : PageModel
    {
        private readonly LabAssignment4.DataAccess.StudentrecordContext _context;

        public DetailsModel(LabAssignment4.DataAccess.StudentrecordContext context)
        {
            _context = context;
        }

        public List<AcademicRecord> AcademicRecord { get; set; } = new List<AcademicRecord>();

        public string getCourseName(string courseID)
        {
            Course courseItem = _context.Courses
                .Where(course => course.Code == courseID)
                .FirstOrDefault()!;

            if (courseItem != null)
                return courseItem.Title;
            return "Course not found";
        }


        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                Student = student;
            }

            // Retrieve academic records based on the student ID
            AcademicRecord = await _context.AcademicRecords
                .Where(ar => ar.StudentId == id)
                .ToListAsync();

            if (AcademicRecord == null || AcademicRecord.Count == 0)
            {
                return NotFound();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostGradeSortDetail()
        {
            if (Student == null)
            {
                return NotFound();
            }

            // Fetch the student record based on the provided ID
            var student = await _context.Students.FindAsync(Student.Id);

            if (student != null)
            {
                // Log or debug
                Console.WriteLine("Grade sort clicked");
                AcademicRecord = await _context.AcademicRecords
                    .Where(ar => ar.StudentId == Student.Id)
                    .ToListAsync();

            }

            return RedirectToPage("./Details");
        }


        public async Task<IActionResult> OnPostCourseSort()
        {
            if (Student == null)
            {
                return NotFound();
            }

            // Fetch the student record based on the provided ID
            var student = await _context.Students.FindAsync(Student.Id);

            if (student != null)
            {
                var academicRecords = await _context.AcademicRecords
                    .Where(ar => ar.StudentId == Student.Id)
                    .ToListAsync();

                AcademicRecord = academicRecords
                    .OrderBy(ar => _context.Courses
                        .Where(c => c.Code == ar.CourseCode)
                        .Select(c => c.Title)
                        .FirstOrDefault())
                    .ToList();

            }

            return RedirectToPage("./Details");
        }
    }
}

