using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementApp.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int MaxNumberOfStudents { get; set; }
        public List<Student> Students { get; set; }
    }
}
