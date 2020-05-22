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
            return View("~/Views/Admin/Groups/Index.cshtml", groupViewModel);
        }
        
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var detail = _groupRepository.GetDetail(id);
            return View("~/Views/Admin/Groups/Detail.cshtml", detail);
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