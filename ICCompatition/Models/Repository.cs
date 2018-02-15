using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ICCompatition.Models
{
    public class Repository<T> : IRepository<T> where T : ExerciseViewModel
    {
        private static Repository<T> exerciseRepo = new Repository<T>();
        public static List<T> Records { get; set; }
        string errorMessage = string.Empty;

        
        public IQueryable<T> Get(int id)
        {
            
            IQueryable<T> query = Records.AsQueryable();
            query = query.Where(x => x.Id == id).AsQueryable();

            return query;
        }

        public IQueryable<T> GetAll()
        {
           
            IQueryable<T> query = Records.AsQueryable();
            
            return query;
        }
        public void Insert(T entity)
        {
            //Regex _dateregex = new Regex(@"(\d{4})-(\d{1,2})-(\d{1,2})");
            //string Durationpattern = @"(?:[1-9][0-9]?|1[01][0-9]|120)";

            Regex _Durationregex = new Regex(@"(?:[1-9][0-9]?|1[01][0-9]|120)");

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (entity.Id != null && entity.ExerciseName != null && entity.ExerciseDateTime != null && entity.DurationInMinutes != null)
            {

                if (Regex.IsMatch(entity.ExerciseDateTime.ToString("yyyy-MM-dd"), @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$"))
                {
                    if (_Durationregex.IsMatch(entity.DurationInMinutes.ToString()))
                    {
                        Records.Add(entity);
                    }
                }
            }
      
        }                

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            Records.Remove(entity);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            else
            {
                Records.Find(x => x.Id == entity.Id).ExerciseName = entity.ExerciseName;
                Records.Find(x => x.Id == entity.Id).ExerciseDateTime = entity.ExerciseDateTime;
                Records.Find(x => x.Id == entity.Id).DurationInMinutes = entity.DurationInMinutes;
            }
        }
    }
}