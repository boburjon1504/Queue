﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Models;
public class TemplatePlaceholder
{
    public string Placeholder { get; set; } = default!;

    public string PlaceholderValue { get; set; } = default!;

    public string? Value { get; set; } = default!;

    public bool IsValid { get; set; }
}
