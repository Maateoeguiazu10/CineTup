using CineTup.Application.Requests;
using CineTup.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Abstractions
{
    public interface IAuthService
    {
        AuthResponse? SingUp(SignUpRequest request);
        AuthResponse? SingIn(SignInRequest request);
    }
}
