using StudentManagementApp.Models;

namespace StudentManagementApp.Service
{
    public interface ICourseService
    {
        Task<List<Student>> GetStudentByCourse(int courseId);
        Task<bool> AssignStudentToCourse(int courseId, int studentId);
        Task<bool> DeleteStudent(int studentId);
    }
}
