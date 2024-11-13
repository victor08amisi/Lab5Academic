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
    public class DetailsModel : PageModel
    {
        private readonly LabAssignment4.DataAccess.StudentrecordContext _context;

        public DetailsModel(LabAssignment4.DataAccess.StudentrecordContext context)
        {
            _context = context;
        }
       
        public AcademicRecord AcademicRecord { get; set; } = default!;
        public List<AcademicRecord > getGradeBasedOnStudentID(string studentId)
        {
          
                // Retrieve academic records for the student by ID
                var newAcademicRecords = _context.AcademicRecords
                    .Where(ar => ar.StudentId == studentId)
                    .ToList();


                return newAcademicRecords;


        }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicrecord = await _context.AcademicRecords.FirstOrDefaultAsync(m => m.StudentId == id);
            if (academicrecord == null)
            {
                return NotFound();
            }
            else
            {
                AcademicRecord = academicrecord;
            }
            return Page();
        }
    }
}
