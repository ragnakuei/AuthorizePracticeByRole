using System.Web.Mvc;
using AuthorizePracticeByRole.Consts;
using AuthorizePracticeByRole.Infra;
using AuthorizePracticeByRole.Infra.Attributes;
using DAL.Repository.@interface;

namespace AuthorizePracticeByRole.Controllers.Admin
{
    [CustomAuthorize(RoleConst.Admin)]
    public class GroupController : BaseController
    {
        private readonly IGroupRepository _groupRepository;
        
        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        
        public ActionResult Index()
        {
            var result = _groupRepository.GetList();
            return View(result);
        }
    }
}