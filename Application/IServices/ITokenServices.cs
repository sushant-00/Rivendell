using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.IServices
{
    public interface ITokenServices
    {
        string  CreateToken(AppUsers user);
    }
}
