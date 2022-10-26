using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class EFDBTypeLessonRepository : ITypeLessonRepository
    {
        private EFDBContext context;

        public EFDBTypeLessonRepository(EFDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<TypeLesson> GetAllTypeLessons() => context.TypeLessons.ToList();

        public TypeLesson GetTypeLessonById(int id) => context.TypeLessons.FirstOrDefault(x => x.Id == id);

        public TypeLesson GetTypeLessonByName(string Name) => context.TypeLessons.FirstOrDefault(x => x.NameType == Name);


    }
}
