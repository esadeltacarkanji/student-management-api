using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.Models;
using StudentManagementApp.Service;

namespace StudentManagementApp.Controllers
{
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService studentRepository)
        {
            _courseService = studentRepository;
        }


        [HttpGet]
        [Route("GetStudents/{courseId}")]
        public async Task<ActionResult<List<Student>>> GetStudentByCourse(int courseId)
        {
            try
            {
                var result = await _courseService.GetStudentByCourse(courseId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("AssignStudentToCourse/{courseId,studentId}")]
        public async Task<IActionResult> AssignStudentToCourse(int courseId, int studentId)
        {
            try
            {
                var assignedstudent = await _courseService.AssignStudentToCourse(courseId, studentId);
                if (assignedstudent)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();

            }
        }

        [HttpDelete]
        [Route("DeleteStudent/{studentId}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            try
            {
                var result = await _courseService.DeleteStudent(studentId);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
