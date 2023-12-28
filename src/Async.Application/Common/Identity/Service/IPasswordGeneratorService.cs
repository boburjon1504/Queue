using Async.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Service;
public interface IPasswordGeneratorService
{
    string GeneratePassword();

    string GetValidatedPassword(string password, User user);
}
