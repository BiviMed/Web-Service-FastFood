using Azure;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WSFastFood.Data;
using WSFastFood.Models.Dtos;
using WSFastFood.Models.Entities;
using WSFastFood.Models.Responses;
using WSFastFood.Services.HashService;

namespace WSFastFood.Services.UsersServices
{
    public class UserService : IUserService
    {
        private readonly FastFoodContext _context;
        private readonly IHashService _hashService;


        public UserService(FastFoodContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }


        public async Task<GeneralResponse> SearchUsers()
        {
            GeneralResponse response = new();
            try
            {
                List<User> lstUsers = await _context.Users.OrderBy(u => u.FirstName).ToListAsync();

                var lst = lstUsers.Select(u => new { u.Id, u.FirstName, u.LastName, u.Email, u.Address, u.Phone, u.Role })
                                  .Distinct()
                                  .OrderBy(u => u.FirstName)
                                  .ToArray();

                response.Data = lst;
                response.Success = 1;                
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        
        public async Task<GeneralResponse> SearchUser(int id)
        {
            GeneralResponse response = new();
            try
            {
                User? user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    response.Message = "Usuario no encontrado";
                    return response;
                }                               
                response.Data = user; 
                response.Success = 1;
            }
            catch (Exception e)
            {
                response.Success = 0;
                response.Message = e.Message;
            }
            return response;
        }


        public async Task<GeneralResponse> AddUser(NewUserDto newUserDto)
        {
            GeneralResponse response = new();
            response = await VerifyEmail(newUserDto.Email);

            if (response.Success == 0)
            {
                return response;
            }            
            _hashService.CreateHashPassword(newUserDto.Password, out string hashPassword, out string salt);
            ConvertToNewUser(newUserDto, hashPassword, salt, out User user);

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                response.Success = 1;
                response.Message = "Usuario agregado con éxito";
            }
            catch (Exception e)
            {
                response.Success = 0;
                response.Message = e.Message;
            }
            return response;
        }


        public async Task<GeneralResponse> EditUser(EditUserDto editUserDto)
        {
            GeneralResponse response = new();
            response = await SearchUser(editUserDto.Id);
            if (response.Success == 0)
            {
                return response;
            }

            User userDB = (User)response.Data!;
            ConvertToEditUser(userDB!, editUserDto, out User editUser);

            try
            {
                _context.Users.Entry(editUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                response.Success = 1;
                response.Message = "Usuario modificado con éxito";
                response.Data = null;
            }
            catch (Exception e)
            {
                response.Success = 1;
                response.Message = e.Message;
                response.Data = null;
            }
            return response;
        }


        private async Task<GeneralResponse> VerifyEmail(string email)
        {
            GeneralResponse response = new();
            try
            {
                var userEmail = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

                if (userEmail != null)
                {
                    response.Message = "Este correo ya se encuentra en uso";
                }else
                {
                    response.Success = 1;
                    response.Message = "Correo disponible";
                }
            }
            catch (Exception e)
            {                
                response.Message = e.Message;
            }
            return response;
        }


        private static void ConvertToNewUser(NewUserDto newUserDto, string hashedPassword, string salt, out User user)
        {
            newUserDto.Role ??= "CLiente";

            if (newUserDto.Role != "Cliente" && newUserDto.Role != "Empleado" && newUserDto.Role != "Admin")
            {
                newUserDto.Role = "Cliente";
            }

            user = new()
            {
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                Email = newUserDto.Email,
                Address = newUserDto.Address,
                Phone = newUserDto.Phone,
                Role = newUserDto.Role,
                Password = hashedPassword,
                Salt = salt
            };
        }

        private static void ConvertToEditUser(User user, EditUserDto editUserDto, out User editUser)
        {
            editUser = user;
            editUser.FirstName = editUserDto.FirstName;
            editUser.LastName = editUserDto.LastName;
            editUser.Address = editUserDto.Address;
            editUser.Phone = editUserDto.Phone;
            if (editUserDto.Role == null)
            {
                editUser.Role = "Cliente";
            }
            else if (editUserDto.Role != "Cliente" && editUserDto.Role != "Empelado" && editUserDto.Role != "Admin" && editUserDto.Role != null)
            {
                editUser.Role = "Cliente";
            }
            else
            {
                editUser.Role = editUserDto.Role!;
            }
        }
    }
}
