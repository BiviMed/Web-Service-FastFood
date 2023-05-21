using WSFastFood.Models.Dtos;
using WSFastFood.Models.Responses;

namespace WSFastFood.Services.UsersServices
{
    public interface IUserService
    {        
        public Task<GeneralResponse> SearchUser(int id);
        public Task<GeneralResponse> SearchUsers();
        public Task<GeneralResponse> AddUser(NewUserDto newUserDto);
        public Task<GeneralResponse> EditUser(EditUserDto editUserDto);
    }
}
