using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Model;
public class SignUpDetails
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string EmailAddress { get; set; } = default!;

    public int Age { get; set; }

    public string? Password { get; set; }

    public bool AutoGeneratePassword { get; set; }
}
