using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatR.Models;
using ChatR.Exceptions;

namespace ChatR.WorkerService
{
    public class UserWorkerService : BaseWorkerService
    {
        /// <summary>
        /// Returns user by it's ID
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>ConnectedUserModel</returns>
        public ConnectedUserModel GetConnectedUserById(int userId)
        {
            var user = UnitOfWork.UserRepository.GetByID(userId);

            if(user != null)
            {
                return new ConnectedUserModel
                {
                    Avatar = user.Avatar,
                    UserID = user.Id,
                    UserName = user.UserName
                };
                
            }
            else
            {
                throw new UserNotFoundException("User with the ID: {0} has not been found.", userId);
            }
            
        }

        /// <summary>
        /// Returns multiple users by their Ids
        /// </summary>
        /// <param name="ids">array of the id's</param>
        /// <returns>List<ConnectedUserModel></returns>
        public List<ConnectedUserModel> GetConnectedUsersByIds(List<int> ids)
        {
            var users = UnitOfWork.UserRepository
                .Get(x => ids.Contains(x.Id))
                .Select(x => new ConnectedUserModel {
                    Avatar = x.Avatar,
                    UserID = x.Id,
                    UserName = x.UserName
                })
                .ToList();

            return users;
                
        }
    }
}