using Firebase.Database;
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
                    Login = item.Object.Login,
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

        //public DB(string path)
        //{
        //    conn = new SQLiteConnection(path);
        //    conn.CreateTable<Lesson>();
        //    conn.CreateTable<User>();
        //    conn.CreateTable<New>();

        //}
        //public List<User> GetUsers()
        //{
        //    return conn.Table<User>().ToList();
        //}
        //public User GetUser(int id)
        //{
        //    return conn.Get<User>(id);
        //}
        //public int SaveUser(User user)
        //{
        //    if (user.UserId != 0)
        //    {
        //        conn.Update(user);
        //        return user.UserId;
        //    }
        //    else
        //    {
        //        return conn.Insert(user);
        //    }
        //}
        public int DeleteUser(int Id)
        {
            return conn.Delete<User>(Id);
        }
        public List<Lesson> GetLessons()
        {
            return conn.Table<Lesson>().ToList();
        }
        public Lesson GetLesson(int Id)
        {
            return conn.Get<Lesson>(Id);
        }
        public int SaveLesson(Lesson lesson)
        {
            if (lesson.LessonId != 0)
            {
                conn.Update(lesson);
                return lesson.LessonId;
            }
            else
            {
                return conn.Insert(lesson);
            }
        }
        public int DeleteBook(int Id)
        {
            return conn.Delete<Lesson>(Id);
        }

        public List<New> GetNews()
        {
            return conn.Table<New>().ToList();
        }
        public New GetNew(int Id)
        {
            return conn.Get<New>(Id);
        }
        public int SaveNew(New news)
        {
            if (news.NewId != 0)
            {
                conn.Update(news);
                return news.NewId;
            }
            else
            {
                return conn.Insert(news);
            }
        }
        public int DeleteNew(int Id)
        {
            return conn.Delete<New>(Id);
        }
    }

  
}
