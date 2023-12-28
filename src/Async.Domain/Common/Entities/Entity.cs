using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Common.Entities;
public abstract class Entity:IEntity
{
    public Guid Id { get; set; }
}
