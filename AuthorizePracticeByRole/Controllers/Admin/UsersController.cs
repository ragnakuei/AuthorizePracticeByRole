using System.Web.Mvc;
using AuthorizePracticeByRole.Consts;
using AuthorizePracticeByRole.Infra;
using AuthorizePracticeByRole.Infra.Attributes;
using AuthorizePracticeByRole.Validators;
using AuthorizePracticeByRole.ViewModels;
using DAL.Repository.@interface;
using SharedLibrary.Entities;

namespace AuthorizePracticeByRole.Controllers.Admin
{
    [CustomAuthorize(RoleConst.Admin)]
    public class UsersController : BaseController
    {
        private readonly IUserValidator  _groupValidator;
        private readonly IUserRepository _userRepository;

        public UsersController(IUserValidator groupValidator, IUserRepository userRepository)
        {
            _groupValidator  = groupValidator;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Index(int? id = null)
        {
            var groupViewModel = new UserViewModel
                                 {
                                     Users = _userRepository.GetList(),
                                     EditId = id,
                                 };
            return View("~/Views/Admin/Users/Index.cshtml", groupViewModel);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var detail = _userRepository.GetDetail(id);
            return View("~/Views/Admin/Users/Detail.cshtml", detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(User newUser)
        {
            _userRepository.New(newUser);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateNew(User newUser)
        {
            var validateResult = _groupValidator.ValidateNew(newUser);
            return Json(validateResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User item)
        {
            if (item.Id != 1)
            {
                _userRepository.Update(item);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id != 1)
            {
                _userRepository.Delete(id);
            }

            return RedirectToAction("Index");
        }
    }
}