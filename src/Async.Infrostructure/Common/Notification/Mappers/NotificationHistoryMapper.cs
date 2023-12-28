using Async.Application.Common.Identity.Notififcation.Models;
using Async.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Notification.Mappers;
public class NotificationHistoryMapper:Profile
{
    public NotificationHistoryMapper()
    {
        CreateMap<EmailMessage, EmailHistory>()
            .ForMember(dest => dest.TemplateId, opt => opt.MapFrom(src => src.Template.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Body));
    }
}
