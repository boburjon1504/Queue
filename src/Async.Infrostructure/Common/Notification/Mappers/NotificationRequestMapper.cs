using Async.Application.Common.Identity.Notififcation.Events;
using Async.Application.Common.Identity.Notififcation.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Notification.Mappers;
public class NotificationRequestMapper:Profile
{
    public NotificationRequestMapper()
    {
        CreateMap<ProcessNotificationEvent, EmailProcessNotificationEvent>();
    }
}
