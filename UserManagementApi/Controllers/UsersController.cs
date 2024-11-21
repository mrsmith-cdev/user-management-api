using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Filters;
using UserManagementApi.ViewModels;
using UserManagementApi.ViewModels.Shared;
using UserManagementCommon.Utilities;
using UserManagementServices.Services;
using Microsoft.Extensions.Options;
using System.Net;
using UserManagementCommon.Models;

namespace UserManagementApi.Controllers
{
    [ServiceFilter(typeof(LoggerAttribute))]

    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ILogger<object> _logger;
        private readonly AppConfig _config;
        public UsersController(IOptions<AppConfig> options, ILoggerFactory loggerFactory)
        {
            _config = options.Value;
            _logger = loggerFactory.CreateLogger<object>();
            _service = new UserService(_config, _logger);
        }
        #region POST & PUT
        [HttpPost("create")]
        public ActionResult<ApiResponse<int>> Post(UserVM vm)
        {
            ApiResponse<int> response = new();
            int code;
            string message;

            try
            {
                int UserId = _service.createUser(vm.ToServiceModel(vm), out code, out message);
                if (UserId > 0)
                {
                    return Ok(response.GetSuccessResponseObject(UserId, message));
                }
                else
                {
                    return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, message));
                }
            }
            catch (Exception exp)
            {
                return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, exp.Message));
            }
        }
        [HttpPut("update")]
        public ActionResult<ApiResponse<bool>> Updateuser(UserVM vm)
        {
            ApiResponse<bool> response = new();
            int code;
            string message;

            try
            {
                bool res = _service.UpdateUser(vm.ToServiceModel(vm), out code, out message);
                if (res)
                {
                    return Ok(response.GetSuccessResponseObject(res, message));
                }
                else
                {
                    return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, message));
                }
            }
            catch (Exception exp)
            {
                return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, exp.Message));
            }
        }
        #endregion

        #region GET
        [HttpGet("search")]
        public ActionResult<ApiGridResponse<UserVM>> Index([FromQuery] SearchRequestModel vm)
        {
            var response = new ApiGridResponse<UserVM>();
            try
            {
                _logger.LogInformation($"Going to fetch Users");
                int totalCount;
                var serviceList = _service.GetUsers(vm, out totalCount);
                var result = new UserVM().FromServiceModelList(serviceList).ToList();

                if (serviceList != null && serviceList.Any())
                {
                    var resp = response.GetSuccessResponseObject(result, Constant.GET_API_SUCCESS_MSG);
                    resp.totalCount = totalCount;
                    return Ok(resp);
                }
                else
                {
                    return Ok(response.GetNullResponseObject());
                }

            }
            catch (Exception exp)
            {
                return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, exp.Message));
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<UserVM>> Get(int id)
        {
            var response = new ApiResponse<UserVM>();
            try
            {
                _logger.LogInformation($"Going to fetch Service");
                var service = _service.GetUserById(id, out string msg);

                if (service != null)
                {
                    var result = new UserVM().FromServiceModel(service);
                    var resp = response.GetSuccessResponseObject(result, Constant.GET_API_SUCCESS_MSG);
                    return Ok(resp);
                }
                else
                {
                    return NotFound(response.GetResponseObject(null, false, msg, (int)HttpStatusCode.NotFound));
                }

            }
            catch (Exception exp)
            {
                return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, exp.Message));
            }
        }

        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<int>> Delete(int id)
        {
            ApiResponse<int> response = new();

            try
            {
                _logger.LogInformation($"Going to fetch Service");
                var service = _service.DeleteUser(id, out int code, out string msg);

                if (service != -1)
                {
                    return Ok(response.GetSuccessResponseObject(service, msg));
                }
                else
                {
                    return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, msg));
                }

            }
            catch (Exception exp)
            {
                return BadRequest(response.GetErrorResponseObject((int)HttpStatusCode.InternalServerError, ErrorCodes.SYSTEM_ERROR, exp.Message));
            }
        }
        #endregion


    }
}
