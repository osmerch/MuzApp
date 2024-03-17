using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using static MuzApp.DbTables;

namespace MuzApp
{
    public class DB
    {
        SQLiteConnection conn;

        public DB(string path)
        {
            conn = new SQLiteConnection(path);
            conn.CreateTable<Lesson>();
            conn.CreateTable<User>();
            conn.CreateTable<New>();

        }
        public List<User> GetUsers()
        {
            return conn.Table<User>().ToList();
        }
        public User GetUser(int id)
        {
            return conn.Get<User>(id);
        }
        public int SaveUser(User user)
        {
            if (user.UserId != 0)
            {
                conn.Update(user);
                return user.UserId;
            }
            else
            {
                return conn.Insert(user);
            }
        }
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
