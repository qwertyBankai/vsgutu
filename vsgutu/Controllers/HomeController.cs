using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PresentationLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using vsgutu.Models;

namespace vsgutu.Controllers
{
    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(_dataManager);
        }

        public IActionResult Index()
        {
            /*Вывод групп*/
            /*List<GroupsModel> _groupsModel = _serviceManager.Groups.GetGroupsList();
            return View(_groupsModel);*/
            List<RolesOfUsersModel> _rolesModel = _serviceManager.Roles.GetRolesList();
            return View(_rolesModel);
        }
        /*[HttpPost]
        public IActionResult Index(GroupsModel model)
        {
            //Добавление
            *//*_serviceManager.Groups.CreateGroup(model);

            return RedirectToAction("Index");*/
            /* УДАЛЕНИЕ ГРУПП*//*
            _serviceManager.Groups.DeleteGroup(model);
            return RedirectToAction("Index");
        }*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
