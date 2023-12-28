using Async.Domain.Common.Entities;
using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Entities;
public class User : Entity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public int Age { get; set; }

    public string EmailAddress { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public bool IsEmailAddressVerified { get; set; }

    public RoleType Role { get; set; }

    public UserSettings? UserSettings { get; set; }
}
