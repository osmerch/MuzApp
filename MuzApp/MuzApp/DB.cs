﻿using Firebase.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static MuzApp.DbTables;
using Firebase.Database.Query;
using System.Linq;


namespace MuzApp
{
    public class DB
    {
        SQLiteConnection conn;

        private readonly FirebaseClient firebaseClient;

        public DB()
        {
            firebaseClient = new FirebaseClient("https://muzicschool-f7f69-default-rtdb.firebaseio.com/");
        }

        //UserData
        public async Task AddUserData(UserDataAuth user)
        {
            await firebaseClient
                .Child("UsersData")
                .PostAsync(user);
        }

        public async Task<List<UserDataAuth>> GetAllUsersData()
        {
            return (await firebaseClient
                .Child("UsersData")
                .OnceAsync<UserDataAuth>())
                .Select(item => new UserDataAuth
                {
                    UserId = item.Object.UserId,
                    Email = item.Object.Email,
                    Password = item.Object.Password,
                    RoleId = item.Object.RoleId,

                }).ToList();
        }

        //Admin
        public async Task AddAdmin(Admin admin)
        {
            await firebaseClient
                .Child("Admin")
                .PostAsync(admin);
        }

        public async Task AddStudent(Student student)
        {
            await firebaseClient
                .Child("Student")
                .PostAsync(student);
        }

        //Course
        public async Task AddCourse(Course course)
        {
            await firebaseClient
                .Child("Course")
                .PostAsync(course);
        }

        public async Task<List<Course>> GetAllCourse()
        {
            return (await firebaseClient
                .Child("Course")
                .OnceAsync<Course>())
                .Select(item => new Course
                {
                    CourseId = item.Object.CourseId,
                    Name = item.Object.Name,
                    Desc = item.Object.Desc

                }).ToList();
        }
        public async Task<List<Teacher_Course>> GetAllCourseTeacher()
        {
            return (await firebaseClient
                .Child("Course")
                .OnceAsync<Teacher_Course>())
                .Select(item => new Teacher_Course
                {
                    Id = item.Object.Id,
                    CourseId = item.Object.CourseId,
                    TeacherId = item.Object.TeacherId
                }).ToList();
        }
        public async Task AddTeacher_Course(Teacher_Course Teacher_Course)
        {
            await firebaseClient
               .Child("Teacher_Course")
               .PostAsync(Teacher_Course);
        }
        //Teacher
        public async Task<List<Teacher>> GetAllTeacgers()
        {
            return (await firebaseClient
                .Child("Teacher")
                .OnceAsync<Teacher>())
                .Select(item => new Teacher
                {
                    UserId = item.Object.UserId,
                    Name = item.Object.Name,
                    Surname = item.Object.Surname,
                    Email = item.Object.Email,
                    Desc = item.Object.Desc

                }).ToList();
        }
        public async Task AddTeacher(Teacher teacher)
        {
            await firebaseClient
                .Child("Teacher")
                .PostAsync(teacher);
        }

        //WorkSchedule
        public async Task AddWorkSchedule(WorkSchedule workSchedule)
        {
            await firebaseClient
                .Child("WorkSchedule")
                .PostAsync(workSchedule);
        }

        public async Task<List<WorkSchedule>> GetAllWorkSchedules()
        {
            return (await firebaseClient
                .Child("WorkSchedule")
                .OnceAsync<WorkSchedule>()).Select(item =>
                {
                    var obj = item.Object;
                    var prop = obj.GetType().GetProperty("Key");
                    if (prop != null)
                    {
                        prop.SetValue(obj, item.Key);
                    }
                    return obj;
                }).ToList();
        }

        //lesson
        public async Task AddLesson(Lesson lesson)
        {
            await firebaseClient
                .Child("Lesson")
                .PostAsync(lesson);
        }

        public async Task<List<Lesson>> GetAllLessons()
        {
            return (await firebaseClient
                .Child("Lesson")
                .OnceAsync<Lesson>()).Select(item =>
                {
                    var obj = item.Object;
                    var prop = obj.GetType().GetProperty("Key");
                    if (prop != null)
                    {
                        prop.SetValue(obj, item.Key);
                    }
                    return obj;
                }).ToList();
        }

        public async Task<UserDataAuth> GetUserDataById(int id)
        {
            var userData = await firebaseClient
                .Child("UsersData")
                .Child(id.ToString())
                .OnceSingleAsync<UserDataAuth>();

            return userData;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await firebaseClient
                .Child("Student")
                .Child(id.ToString())
                .OnceSingleAsync<Student>();

            return student;
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            var teacher = await firebaseClient
                .Child("Teacher")
                .Child(id.ToString())
                .OnceSingleAsync<Teacher>();

            return teacher;
        }
    }
}

