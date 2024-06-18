﻿using Library.application.UseCases;
using Library.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.UseCases.Commands.Users
{
    public interface IRegisterUserCommand : ICommand<RegisterUserDto>
    {
    }
}
