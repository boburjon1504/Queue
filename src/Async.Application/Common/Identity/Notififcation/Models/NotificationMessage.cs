using Async.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Models;
public abstract class NotificationMessage
{
    public string Body { get; set; } = default!;

    public NotificationTemplate Template { get; set; } = default!;

    public Dictionary<string, string> Variables { get; set; } = default!;

    public bool IsSuccessful { get; set; }

    public string? ErrorMessage { get; set; }
}
