using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AccuV.UI.Security;
using AccuVAPI.Contracts;
using AccuVAPI.Operations;

namespace AccuV.UI.Controllers.Api
{
    [ClaimsAuthorize("Admin", AccessPermissions.Admin)]
    public class DomainUsersController : ApiController
    {
        private readonly IActiveDirectoryOperations _operations;

        public DomainUsersController(IActiveDirectoryOperations operations)
        {
            _operations = operations;
        }

        public async Task<IEnumerable<ActiveDirectoryUser>> Get()
        {
            return await _operations.GetAllAsync();
        }

        //public ActiveDirectoryUser GetById(string userId)
        //{
            
        //}

    }
}
