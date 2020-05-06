using AuthorizePracticeByRole.Consts;
using AuthorizePracticeByRole.Infra;
using AuthorizePracticeByRole.Infra.Attributes;
 
namespace AuthorizePracticeByRole.Controllers.Admin
{
    [CustomAuthorize(RoleConst.Admin)]
    public class Group : BaseController
    {
        // private readonly IGroupRepository _groupRepository;
        //
        // public Group(IGroupRepository groupRepository)
        // {
        //     _groupRepository = groupRepository;
        // }
        //
        // public ActionResult Index()
        // {
        //     var result = _groupRepository.GetList();
        //     return View(result);
        // }
    }
}