using System.Web.Mvc;
using AuthorizePracticeByRole.Consts;
using AuthorizePracticeByRole.Infra;
using AuthorizePracticeByRole.Infra.Attributes;
using AuthorizePracticeByRole.Validators;
using AuthorizePracticeByRole.ViewModels;
using DAL.Entities;
using DAL.Repository.@interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AuthorizePracticeByRole.Controllers.Admin
{
    [CustomAuthorize(RoleConst.Admin)]
    public class GroupsController : BaseController
    {
        private readonly IGroupValidator _groupValidator;
        private readonly IGroupRepository _groupRepository;
        
        public GroupsController(IGroupValidator groupValidator,IGroupRepository groupRepository)
        {
            _groupValidator = groupValidator;
            _groupRepository = groupRepository;
        }
        
        [HttpGet]
        public ActionResult Index(int? id = null)
        {
            var groupViewModel = new GroupViewModel
                                 {
                                     Groups = _groupRepository.GetList(),
                                     EditId = id,
                                 };
            return View(groupViewModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Group newGroup)
        {
            _groupRepository.New(newGroup);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateNew(Group newGroup)
        {
            var validateResult = _groupValidator.ValidateNew(newGroup);
            return Json(validateResult);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Group item)
        {
            _groupRepository.Update(item);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _groupRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}