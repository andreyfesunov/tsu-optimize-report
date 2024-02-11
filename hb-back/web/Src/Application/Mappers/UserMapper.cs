using web.Domain.Models;
using DALUser = web.Infrastructure.Models.User;

namespace web.Application.Mappers;

public class UserMapper : IMapper<DALUser, UserModel>
{
    public UserModel MapFrom(DALUser param)
    {
        return new UserModel(param.Id.ToString());
    }

    public DALUser MapTo(UserModel param)
    {
        return new DALUser(param.Id);
    }
}