using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;

namespace StudentManagementApp.Service
{
    public class CourseService : ICourseService
    {
        private readonly StudentDbContext _context;
        public CourseService(StudentDbContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetStudentByCourse(int courseId)
        {
            var student = await _context.Course.Include(a => a.Students).FirstOrDefaultAsync(a => a.CourseId == courseId);
            return student?.Students ?? new List<Student>();
        }

        public async Task<bool> AssignStudentToCourse(int courseId, int studentId)
        {
            var course = await _context.Course.Include(a => a.Students).FirstOrDefaultAsync(a => a.CourseId == courseId);
            var student = await _context.Students.FindAsync(studentId);
            if (course?.Students.Count >= course?.MaxNumberOfStudents || course == null || student == null)
            {
                return false;
            }
            course?.Students.Add(student);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteStudent(int studentId)
        {
            var studentToDelete = await _context.Students.Where(x => x.Id == studentId).FirstOrDefaultAsync();
            if (studentToDelete != null)
            {
                _context.Students.Remove(studentToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
