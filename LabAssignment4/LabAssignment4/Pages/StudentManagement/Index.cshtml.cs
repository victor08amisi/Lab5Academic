using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LabAssignment4.DataAccess;

namespace LabAssignment4.Pages.StudentManagement
{
    public class IndexModel : PageModel
    {

        public List<AcademicRecord> AcademicRecords { get; set; } = new List<AcademicRecord>();
        
        [BindProperty] 
        public string? studentIDName { get; set; }

        private readonly LabAssignment4.DataAccess.StudentrecordContext _context;

        public IndexModel(LabAssignment4.DataAccess.StudentrecordContext context)
        {
            _context = context;
        }
        
        public int GetTotalCourse(string studentUniqueID)
        {
            return _context.AcademicRecords
                .Count(ar => ar.StudentId == studentUniqueID);
        }

        public double GetAverageGrades(string studentId)
        {
            var averageGrade = _context.AcademicRecords
                .Where(ar => ar.StudentId == studentId)
                .Average(ar => (double?)ar.Grade) ?? 0.0;

            return averageGrade;
        }

        public List<int> GetAllGrades()
        {
            var allGrades = _context.AcademicRecords
                .Select(ar => ar.Grade.GetValueOrDefault()) // 
                .ToList();

            return allGrades;
        }

       

        public IList<Student> Student { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();

        }

        public async Task OnPostNameSort()
        {
           
            Student = await _context.Students
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task OnPostGradeSort()
        {
            // Query the database to get students ordered by their grades
            Student = await _context.Students
                .OrderBy(s => GetAllGrades()) // Assuming GetAverageGrade method to fetch grades
                .ToListAsync();
        }


        public async Task OnPostAverageGradeSort()
        {
          
            // Query the database to get students ordered by their average grade
            Student = await _context.Students
                .Select(s => new 
                {
                    Student = s,
                    AverageGrade = _context.AcademicRecords
                        .Where(ar => ar.StudentId == s.Id)
                        .Average(ar => ar.Grade ?? 0)
                })
                .OrderBy(s => s.AverageGrade) 
                .Select(s => s.Student) 
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostDeleteStudentAsync()
        {
            if (studentIDName == null)
            {
                return NotFound();
            }

            // Fetch the student record based on the provided ID
            var student = await _context.Students.FindAsync(studentIDName);

            if (student != null)
            {
                // Fetch and delete all academic records associated with the student ID
                var academicRecords = _context.AcademicRecords
                    .Where(ar => ar.StudentId == studentIDName);

                _context.AcademicRecords.RemoveRange(academicRecords);

                // After deleting the academic records, delete the student
                _context.Students.Remove(student);

                // Save changes to apply both deletions
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
    
}