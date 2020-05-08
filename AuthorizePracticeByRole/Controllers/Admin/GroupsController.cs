using System.Web.Mvc;
using AuthorizePracticeByRole.Consts;
using AuthorizePracticeByRole.Infra;
using AuthorizePracticeByRole.Infra.Attributes;
using AuthorizePracticeByRole.ViewModels;
using DAL.Entities;
using DAL.Repository.@interface;

namespace AuthorizePracticeByRole.Controllers.Admin
{
    [CustomAuthorize(RoleConst.Admin)]
    public class GroupsController : BaseController
    {
        private readonly IGroupRepository _groupRepository;
        
        public GroupsController(IGroupRepository groupRepository)
        {
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
        public ActionResult New(Group item)
        {
            // _groupRepository.New(item);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Group item)
        {
            _groupRepository.Update(item);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            _groupRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}