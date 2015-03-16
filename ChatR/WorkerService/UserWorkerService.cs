using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatR.Models;

namespace ChatR.WorkerService
{
    public class UserWorkerService : BaseWorkerService
    {
        public List<ConnectedUserModel> GetUsersByIds(List<int> ids)
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