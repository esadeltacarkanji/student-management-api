using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;
using StudentManagementApp.Service;

namespace StudentManagementAppTests.Service
{
    public class CourseServiceTests
    {
        private DbContextOptions<StudentDbContext> _options;

        public CourseServiceTests()
        {
            _options = new DbContextOptionsBuilder<StudentDbContext>()
                .UseInMemoryDatabase(databaseName: "MyDb")
                .Options;

            using (var context = new StudentDbContext(_options))
            {
                context.Course.Add(new Course
                {
                    CourseId = 1,
                    Name = "Data Structure",
                    MaxNumberOfStudents = 25,
                    Students = new List<Student>
                {
                    new Student { Id = 1, FirstName = "Esa" , Age = 23 , Email ="esa@gmail.com" , Gender = "Female", LastName = "Carkanji"},
                    new Student { Id = 2, FirstName = "Mateo", Age = 19, Email ="mateo@gmail.com" , Gender = "Male", LastName = "Lname" }
                }
                });

                context.Course.Add(new Course
                {
                    CourseId = 2,
                    Name = "Calculus",
                    MaxNumberOfStudents = 30,
                    Students = new List<Student>
                {
                     new Student { Id = 3, FirstName = "Ana" , Age = 21 , Email ="ana@gmail.com" , Gender = "Female", LastName = "Doka"},
                     new Student { Id = 4, FirstName = "Beni", Age = 20, Email ="beni@gmail.com" , Gender = "Female", LastName = "Tirana" }
                }
                });

                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetStudentByCourse_ReturnsStudents()
        {
            using (var context = new StudentDbContext(_options))
            {

                var service = new CourseService(context);

                var students = await service.GetStudentByCourse(1);

                Assert.Equal(2, students.Count);
                Assert.Equal("Esa", students[0].FirstName);
                Assert.Equal("Mateo", students[1].FirstName);
            }
        }


        [Fact]
        public async Task GetStudentByCourse_ReturnsNull()
        {
            using (var context = new StudentDbContext(_options))
            {

                var service = new CourseService(context);

                var students = await service.GetStudentByCourse(2);

                Assert.Equal(0, students.Count);
            }
        }

        [Fact]
        public async Task AssignStudentToCourse_ReturnsTrue()
        {
            using (var context = new StudentDbContext(_options))
            {
                var service = new CourseService(context);

                var students = await service.AssignStudentToCourse(1, 3);

                Assert.True(students);

            }
        }

        [Fact]
        public async Task AssignStudentToCourse_ReturnsFalse()
        {
            using (var context = new StudentDbContext(_options))
            {
                var service = new CourseService(context);

                var students = await service.AssignStudentToCourse(2, 456);

                Assert.False(students);
            }
        }

        [Fact]
        public async Task DeleteStudent_RemovesStudent()
        {
            using (var context = new StudentDbContext(_options))
            {

                var service = new CourseService(context);

                var students = await service.DeleteStudent(2);

                Assert.True(students);
            }
        }

        [Fact]
        public async Task DeleteStudent_DoesNotRemovesStudent()
        {
            using (var context = new StudentDbContext(_options))
            {
                var service = new CourseService(context);

                var students = await service.DeleteStudent(10);

                Assert.False(students);
            }
        }
    }

}

