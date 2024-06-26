﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MuzApp
{
    public class DbTables
    {
        public class UserDataAuth
        {
            [PrimaryKey]
            public int UserId { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public int RoleId { get; set; }
           
        }
        public class Student
        {
            [PrimaryKey]
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }
        }
        public class StudentCourse
        {
            public int StudentId { get; set; }
            public int CourseId { get; set; }
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
            public string CourseType {  get; set; }
            public string ImageUrl { get; set; }

        }
        public class WorkSchedule
        {
            [PrimaryKey, AutoIncrement]
            public int TeacherId { get; set; }
            public DayOfWeek Day { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
        }
        public class Lesson
        {
            [PrimaryKey, AutoIncrement]
            public int LessonId { get; set; }
            public int TeacherId { get; set; }
            public int CourseId { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan StartTime { get; set; }
            public TimeSpan EndTime { get; set; }
            public string Status { get; set; }  
            public string Room { get; set; }    
        }
        public class New
        {
            public string Id { get; set; }
            public DateTime Date { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
        }
    }
}
