using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DataManager
    {
        private IUsersRepository _usersRepository;
        private IScoreRepository _scoreRepository;
        private IRolesOfUsersRepository _rolesOfUsersRepository;
        private ILessonRepository _lessonRepository;
        private IGroupsRepository _groupsRepository;
        private IDisciplineRepository _disciplineRepository;
        private ITypeLessonRepository _typeLessonRepository;
        private IEKRepository _eKRepository;
        private ISessionScoreRepository _sessionScoreRepository;

        public DataManager(IUsersRepository usersRepository, IScoreRepository scoreRepository,
                           IRolesOfUsersRepository rolesOfUsersRepository, ILessonRepository lessonRepository,
                           IGroupsRepository groupsRepository, IDisciplineRepository disciplineRepository,
                           ITypeLessonRepository typeLessonRepository, IEKRepository eKRepository,
                           ISessionScoreRepository sessionScoreRepository)
        {
            _usersRepository = usersRepository;
            _scoreRepository = scoreRepository;
            _rolesOfUsersRepository = rolesOfUsersRepository;
            _lessonRepository = lessonRepository;
            _groupsRepository = groupsRepository;
            _disciplineRepository = disciplineRepository;
            _typeLessonRepository = typeLessonRepository;
            _eKRepository = eKRepository;
            _sessionScoreRepository = sessionScoreRepository;
        }

        public IUsersRepository Users { get { return _usersRepository; } }
        public IScoreRepository Scories { get { return _scoreRepository; } }
        public IRolesOfUsersRepository RolesOfUsers { get { return _rolesOfUsersRepository; } }
        public ILessonRepository Lessons { get { return _lessonRepository; } }
        public IGroupsRepository Groups { get { return _groupsRepository; } }
        public IDisciplineRepository Disciplines { get { return _disciplineRepository; } }
        public ITypeLessonRepository TypeLesson { get { return _typeLessonRepository; } }
        public ISessionScoreRepository SessionScore { get { return _sessionScoreRepository; } }
        public IEKRepository Ek { get { return _eKRepository; } }
    
    }
}
