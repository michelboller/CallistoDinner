﻿using CallistoDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallistoDinner.Application.Services.Authentication
{
    public record AuthenticationResult(User User, string Token);
}
