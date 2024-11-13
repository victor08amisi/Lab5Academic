using System;
using System.Collections.Generic;

namespace LabAssignment4.DataAccess;

public  class AcademicRecord
{
    public string CourseCode { get; set; } = null!;

    public string StudentId { get; set; } = null!;

    public int? Grade { get; set; }

    public virtual Course CourseCodeNavigation { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
