using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzApp
{
    public class DbTables
    {
        public class UserDataAuth
        {
            [PrimaryKey]
            public int UserId { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public int RoleId { get; set; }
        }
        public class Student
        {
            [PrimaryKey]
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public DateTime DateOfBirth { get; set; }
        }

        public class Teacher
        {
            [PrimaryKey]
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Desc { get; set; }
        }
        public class Admin
        {
            [PrimaryKey]
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
        }
        public class User
        {
            [PrimaryKey,  AutoIncrement]
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public DateTime DateOfBirth { get; set; }
        }

        public class Teacher_Course
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public int TeacherId { get; set; }
            public int CourseId { get; set; }

        }

        public class Course
        {
            [PrimaryKey, AutoIncrement]
            public int CourseId { get; set; }
            public string Name { get; set; }
            public string Desc {  get; set; }

        }
        public class Lesson
        {
            [PrimaryKey, AutoIncrement]
            public int LessonId { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Subject { get; set; }
            public string TeacherName { get; set; }
        }

        public class New
        {
            [PrimaryKey, AutoIncrement]
            public int NewId { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
        }
    }
}
