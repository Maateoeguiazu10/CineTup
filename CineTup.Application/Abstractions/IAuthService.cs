using CineTup.Application.Requests;
using CineTup.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResponse> SingUp(SignUpRequest request);
        Task<AuthResponse> SingIn(SignInRequest request);
    }
}
