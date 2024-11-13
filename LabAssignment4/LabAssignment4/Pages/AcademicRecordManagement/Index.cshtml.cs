using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabAssignment4.DataAccess;

namespace LabAssignment4.Pages.AcademicRecordManagement
{
    public class IndexModel : PageModel
    {
        private readonly LabAssignment4.DataAccess.StudentrecordContext _context;

        public IndexModel(LabAssignment4.DataAccess.StudentrecordContext context)
        {
            _context = context;
        }

        public IList<AcademicRecord> AcademicRecord { get; set; } = default!;

        public async Task OnGetAsync()
        {
            AcademicRecord = await _context.AcademicRecords
                .Include(a => a.CourseCodeNavigation)
                .Include(a => a.Student)
                .ToListAsync();
        }

        // Add sorting functionality
        public async Task OnPostCourseSort()
        {
            AcademicRecord = await _context.AcademicRecords
                .Include(a => a.CourseCodeNavigation)
                .Include(a => a.Student)
                .OrderByDescending(a => a.CourseCodeNavigation.Title)
                .ToListAsync();
        }

        public async Task OnPostNameSort()
        {
            AcademicRecord = await _context.AcademicRecords
                .Include(a => a.CourseCodeNavigation)
                .Include(a => a.Student)
                .OrderByDescending(a => a.Student.Name)
                .ToListAsync();
        }

        public async Task OnPostGradeSort()
        {
            AcademicRecord = await _context.AcademicRecords
                .Include(a => a.CourseCodeNavigation)
                .Include(a => a.Student)
                .OrderByDescending(a => a.Grade)
                .ToListAsync();
        }    public async Task<IActionResult>OnPostDeleteAStudentAsync(string studentId, string courseCode)
                 {
                     Console.WriteLine($"Received studentId: {studentId}, courseCode: {courseCode}");
                     var academicRecord = await _context.AcademicRecords
                         .FirstOrDefaultAsync(ar => ar.StudentId == studentId && ar.CourseCode == courseCode);
         
                     if (academicRecord != null)
                     {
                         _context.AcademicRecords.Remove(academicRecord);
                         await _context.SaveChangesAsync();
                     }
         
                     return RedirectToPage("./Index");
                 }

        
    

    }

}
