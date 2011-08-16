using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exchange.Models
{
    public interface ICourseFilter
    {
        decimal Course { get; set; }
        decimal CourseOrient { get; set; }
        bool CourseMorSign { get; set; }
    }
}
