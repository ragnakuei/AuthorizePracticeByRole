using System.Web.Mvc;
using AuthorizePracticeByRole.Consts;
using AuthorizePracticeByRole.Infra;
using AuthorizePracticeByRole.Infra.Attributes;
using AuthorizePracticeByRole.Validators;
using AuthorizePracticeByRole.ViewModels;
using DAL.Entities;
using DAL.Repository.@interface;

namespace AuthorizePracticeByRole.Controllers.Admin
{
    [CustomAuthorize(RoleConst.Admin)]
    public class RolesController : BaseController
    {
        private readonly IRoleValidator  _groupValidator;
        private readonly IRoleRepository _groupRepository;
        
        public RolesController(IRoleValidator groupValidator, IRoleRepository groupRepository)
        {
            _groupValidator  = groupValidator;
            _groupRepository = groupRepository;
        }
        
        [HttpGet]
        public ActionResult Index(int? id = null)
        {
            var groupViewModel = new RoleViewModel
                                 {
                                     Roles = _groupRepository.GetList(),
                                     EditId = id,
                                 };
            return View("~/Views/Admin/Roles/Index.cshtml", groupViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Role newRole)
        {
            _groupRepository.New(newRole);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateNew(Role newRole)
        {
            var validateResult = _groupValidator.ValidateNew(newRole);
            return Json(validateResult);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role item)
        {
            if (item.Id != 1)
            {
                _groupRepository.Update(item);
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id != 1)
            {
                _groupRepository.Delete(id);
            }
            
            return RedirectToAction("Index");
        }
    }
}