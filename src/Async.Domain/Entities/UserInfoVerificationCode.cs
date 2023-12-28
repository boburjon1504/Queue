using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Entities;
public class UserInfoVerificationCode:VerificationCode
{
    public UserInfoVerificationCode()
    {
        Type = VerificationType.UserInfoVerificationCode;
    }

    public Guid UserId { get; set; }
}
