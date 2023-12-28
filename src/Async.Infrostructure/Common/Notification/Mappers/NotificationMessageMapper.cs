using Async.Application.Common.Identity.Notififcation.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Notification.Mappers;
public class NotificationMessageMapper:Profile
{
    public NotificationMessageMapper()
    {
        CreateMap<EmailProcessNotificationEvent, EmailMessage>();
    }
}
