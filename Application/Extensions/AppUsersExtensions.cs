using Application.Dtos;
using Application.IServices;
using Application.Services;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Extensions
{
    public static class AppUsersExtensions
    {
        public static UserDTO ToDto(this AppUsers user , ITokenServices tokenServices )
        {
            return new UserDTO
            {
                Id = user.Id,
                displayName = user.DisplayName,
                email = user.Email,
                Token = tokenServices.CreateToken(user)

            };

        }
    }
}
