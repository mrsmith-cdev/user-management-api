using UserManagementDBModel.Data;
using UserManagementDBModel.EF.Models;
using UserManagementServices.ServiceModels;
using Microsoft.Extensions.Logging;
using UserManagementCommon.Utilities;
using System.Net;
using Microsoft.EntityFrameworkCore;
using UserManagementServices.Shared;
using UserManagementCommon.Models;

namespace UserManagementServices.Services
{
    public class UserService : BaseService<UserSM, User>
    {
        private readonly AppConfig _appConfig;
        private ILogger _logger;

        public UserService(AppConfig appConfig, ILogger logger) : base(appConfig)
        {
            this._appConfig = appConfig;
            _logger = logger;
        }

        public int createUser(UserSM sm, out int code, out string message)
        {
            try
            {
                if (sm.Age < 18)
                {
                    _logger.LogInformation($"CustomLog:UserService:Failed to create User Age is less than 18");
                    code = (int)HttpStatusCode.BadRequest;
                    message = "Faild to create User";
                    return -1;
                }
                if (uow.RepositoryAsync<User>().Queryable()
                    .Any(u => u.Email == sm.Email))
                {
                    _logger.LogInformation($"CustomLog:UserService:Failed to create User Email is not unique");
                    code = (int)HttpStatusCode.BadRequest;
                    message = "Faild to create User";
                    return -1;
                }
                Insert(sm);
                uow.SaveChanges();
                int userID = Get().Id;
                if (userID > 0)
                {
                    _logger.LogInformation($"CustomLog:UserService: user Created, User Id: {userID}");
                    code = (int)HttpStatusCode.OK;
                    message = "User Created Successfully";
                    return userID;
                }
                else
                {
                    _logger.LogInformation($"CustomLog:UserService:Failed to create User");
                    code = (int)HttpStatusCode.BadRequest;
                    message = "Faild to create User";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:UserService: Error Occured while creating User. Exp: {ex}");
                message = $"Faild to create User {ex.Message}";
                code = (int)HttpStatusCode.BadRequest;
                return -1;
            }
        }

        public int DeleteUser(int Id, out int code, out string message)
        {
            try
            {
                Delete(Id);
                uow.SaveChanges();
                _logger.LogInformation($"CustomLog:UserService: User deleted, user Id: {Id}");
                code = (int)HttpStatusCode.OK;
                message = "User Deleted Successfully";
                return 1;

            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:UserService: Error Occured while deleting User. Exp: {ex}");
                message = $"Faild to delete User {ex.Message}";
                code = (int)HttpStatusCode.BadRequest;
                return -1;
            }
        }

        public List<UserSM> GetUsers(SearchRequestModel sm, out int totalCount)
        {
            try
            {
                int recordsToSkip = 0;
                var query = uow.RepositoryAsync<User>().Queryable();

                if (sm != null && sm.pageNumber > 0 && sm.pageSize > 0)
                {
                    recordsToSkip = (sm.pageNumber - 1) * sm.pageSize;
                }
                sm.pageSize = 10;
                totalCount = query.CountAsync().Result;
                var result = query.Skip(recordsToSkip).OrderByDescending(x => x.Id)
                     .Take(sm.pageSize)
                     .ToList();
                return new UserSM().FromDataModelList(result).ToList();
            }
            catch (Exception exp)
            {
                _logger.LogError($"CustomLog:UserService: Error Occured while fetching Users. Exp: {exp}");
                throw;
            }
        }

        public UserSM? GetUserById(int id, out string msg)
        {
            try
            {
                var data = uow.RepositoryAsync<User>().Queryable()
                    .FirstOrDefault(u => u.Id == id);
                if (data != null)
                {
                    msg = "user found successfully";
                    return new UserSM().FromDataModel(data);
                }
                else
                    msg = "User not found";

                return null;

            }
            catch (Exception ex)
            {
                _logger.LogError($"CustomLog:UserService: Error Occured while GetUser. Exp: {ex}");
                msg = "user not found";
                return null;
            }
        }

        public bool UpdateUser(UserSM sm, out int code, out string message)
        {
            try
            {
                var userDB = uow.RepositoryAsync<User>().Queryable()
                    .FirstOrDefault(x => x.Id == sm.Id);

                if (userDB != null)
                {
                    
                    userDB.PhoneNumber = sm.PhoneNumber;
                    userDB.FirstName = sm.FirstName;
                    userDB.LastName = sm.LastName; 
                    userDB.Email = sm.Email;
                    userDB.DateOfBirth = sm.DateOfBirth;


                    uow.RepositoryAsync<User>().Update(userDB);
                    int numOfRecord = uow.SaveChanges();
                    code = (int)HttpStatusCode.OK;
                    message = "User Updated Successfully";
                    return true;
                }
                else
                {
                    _logger.LogInformation($"CustomLog:UpdateUser:Couldn't find client with User ID: {sm.Id}");
                    code = (int)HttpStatusCode.BadRequest;
                    message = $"Couldn't find user with User id: {sm.Id}";
                    return false;
                }
            }
            catch (Exception exp)
            {
                _logger.LogError($"CustomLog:UserService: Error Occured while Updating Job with ClientID: {sm.Id}. Exp: {exp}");
                code = (int)HttpStatusCode.BadRequest;
                message = $"Error Occured while Updating User with UserID: {sm.Id}";
                throw;
            }
        }
    }
}
