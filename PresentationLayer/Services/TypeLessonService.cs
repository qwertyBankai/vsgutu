using BusinessLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class TypeLessonService
    {
        private DataManager _dataManager;

        public TypeLessonService(DataManager dataManager)
        {
            this._dataManager = dataManager;
        }

        public List<TypeLessonModel> GetTypeLessons()
        {
            var _dataDB = _dataManager.TypeLesson.GetAllTypeLessons();
            List<TypeLessonModel> _modelList = new List<TypeLessonModel>();
            foreach (var i in _dataDB)
            {
                _modelList.Add(TypeLessonModelDBToViewById(i.Id));
            }
            return _modelList;
        }
        //??
        public TypeLessonModel TypeLessonModelDBToViewById(int idType)
        {
            var _model = new TypeLessonModel()
            {
                TypeLesson = _dataManager.TypeLesson.GetTypeLessonById(idType),

            };

            return _model;
        }
    }
}
