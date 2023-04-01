using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentManagementApp.Controllers;
using StudentManagementApp.Models;
using StudentManagementApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAppTests.Controllers
{
    public class CourseControllerTests
    {
        private readonly CourseController _sut;
        private readonly Mock<ICourseService> _courseService;

        public CourseControllerTests()
        {
            _courseService = new Mock<ICourseService>();
            _sut = new CourseController(_courseService.Object);
        }

        [Fact]
        public async Task CourseController_GetStudentByCourse_Returns200Status()
        {
            int courseId = 1;
            var students = new List<Student> { new Student() };
            _courseService.Setup(a => a.GetStudentByCourse(courseId)).ReturnsAsync(students);

            var result = await _sut.GetStudentByCourse(courseId);

            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.Equal(students, okResult.Value);
        }

        [Fact]
        public async Task CourseController_GetStudentByCourse_Returns404Status()
        {

            int courseId = 154;
            List<Student> students = null;
            _courseService.Setup(a => a.GetStudentByCourse(courseId)).ReturnsAsync(students);

            var result = await _sut.GetStudentByCourse(courseId);

            Assert.IsType<NotFoundResult>(result.Result);
            var notFoundResult = result.Result as NotFoundResult;
            Assert.Equal(404, StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task CourseController_GetStudentByCourse_ReturnsBadRequest()
        {
            int courseId = 2;
            _courseService.Setup(a => a.GetStudentByCourse(courseId)).ThrowsAsync(new Exception());

            var result = await _sut.GetStudentByCourse(courseId);

            Assert.IsType<BadRequestResult>(result.Result);
            var badRequest = result.Result as BadRequestResult;
            Assert.Equal(400, StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CourseController_AssignStudentToCourse_Returns204Status()
        {
            int courseId = 1;
            int studentId = 1;
            _courseService.Setup(a => a.AssignStudentToCourse(courseId, studentId)).ReturnsAsync(true);

            var result = await _sut.AssignStudentToCourse(courseId, studentId);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, StatusCodes.Status204NoContent);
        }


        [Fact]
        public async Task CourseController_AssignStudentToCourse_Returns404Status()
        {
            int courseId = 1;
            int studentId = 1;
            _courseService.Setup(a => a.AssignStudentToCourse(courseId, studentId)).ReturnsAsync(false);

            var result = await _sut.AssignStudentToCourse(courseId, studentId);

            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task CourseController_AssignStudentToCourse_ReturnsBadRequest()
        {
            int courseId = 1;
            int studentId = 1;
            _courseService.Setup(a => a.AssignStudentToCourse(courseId, studentId)).ThrowsAsync(new Exception());

            var result = await _sut.AssignStudentToCourse(courseId, studentId);

            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task CourseController_DeleteStudent_Returns204Status()
        {
            int courseId = 1;
            _courseService.Setup(a => a.DeleteStudent(courseId)).ReturnsAsync(true);

            var result = await _sut.DeleteStudent(courseId);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task CourseController_DeleteStudent_Returns404Status()
        {
            int courseId = 1;
            _courseService.Setup(a => a.DeleteStudent(courseId)).ReturnsAsync(false);

            var result = await _sut.DeleteStudent(courseId);

            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, StatusCodes.Status404NotFound);
        }


        [Fact]
        public async Task CourseController_DeleteStudent_ReturnsBadRequest()
        {
            int courseId = 1;
            _courseService.Setup(a => a.DeleteStudent(courseId)).ThrowsAsync(new Exception());

            var result = await _sut.DeleteStudent(courseId);

            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, StatusCodes.Status400BadRequest);
        }

    }
}
