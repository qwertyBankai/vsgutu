using BusinessLayer;
using PresentationLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class ServiceManager
    {
        DataManager _dataManager;
        private GroupsService _groupsService;
        private RolesOfUsersService _rolesOfUsers;
        private DisciplineService _disciplineService;
        private LessonService _lessonService;
        private TypeLessonService _typeLessonService;
        private ScoreService _scoreService;
        private UsersService _userService;
        private SessionScoreService _sessionScoreService;
        private EKService _EKService;
        public ServiceManager(DataManager dataManager)
        {
            _dataManager = dataManager;
            _groupsService = new GroupsService(_dataManager);
            _rolesOfUsers = new RolesOfUsersService(_dataManager);
            _disciplineService = new DisciplineService(_dataManager);
            _typeLessonService = new TypeLessonService(_dataManager);
            _lessonService = new LessonService(_dataManager);
            _scoreService = new ScoreService(dataManager);
            _userService = new UsersService(dataManager);
            _sessionScoreService = new SessionScoreService(dataManager);
            _EKService = new EKService(dataManager);

        }

        public GroupsService Groups { get { return _groupsService; } }
        public RolesOfUsersService Roles { get { return _rolesOfUsers; } }

        public DisciplineService Disciplines { get { return _disciplineService; } }

        public TypeLessonService TypeLesson { get { return _typeLessonService; } }

        public LessonService Lessons { get { return _lessonService; } }
        public ScoreService Scories { get { return _scoreService; } }
        public UsersService Users { get { return _userService; } }

        public SessionScoreService Session {get{return _sessionScoreService; } }

        public EKService EK { get { return _EKService; } }
    }
}
