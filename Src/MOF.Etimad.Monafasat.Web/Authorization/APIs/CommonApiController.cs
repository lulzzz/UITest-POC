using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MOF.Etimad.Monafasat.Web.App_Start;

namespace MOF.Etimad.Monafasat.Web.APIs
{
   [Produces("application/json")]
   [TypeFilter(typeof(AuthenticatedAttribute))]
   public class CommonApiController : Controller
    {
      #region Properties
      private readonly ILogger<CommonApiController> _logger;
      private readonly IMapper _mapper;
      #endregion

      #region Constructors
      public CommonApiController(ILogger<CommonApiController> logger , IMapper mapper)
      {
         _logger = logger;
         _mapper = mapper;
      }
      #endregion

      #region Apis
      /// <summary>
      /// get all Gov Entities
      /// </summary>
      /// <returns>List of gov entities</returns>
      [HttpGet]
      [Route("api/common/Test")]
      public ActionResult Test()
      {
         return Ok();
      }

      #endregion

      #region Helper Methods

      #endregion
   }
}
