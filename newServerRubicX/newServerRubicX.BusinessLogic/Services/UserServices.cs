using AutoMapper;
using Microsoft.EntityFrameworkCore;
using newServerRubicX.BusinessLogic.Core.Interfaces;
using newServerRubicX.BusinessLogic.Core.Models;
using newServerRubicX.DataAccess.Core.Interfaces.DbContext;
using newServerRubicX.DataAccess.Core.Models;
using Share.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newServerRubicX.BusinessLogic.Services
{
    public class UserService : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IRubicContext _context;

        public UserService(IMapper mapper, IRubicContext context)
        {
            _mapper = mapper;
            _context = context;
        }
       
        public async Task<UserInformationBlo> AuthWithEmail(string email, string password)
        {
            UserRto user =await _context.Users.FirstOrDefaultAsync(p => p.Email == email && p.Password == password);

            if (user == null)
                throw new NotFoundException($"Пользователь с почтой {email} не найден");

            return await ConvertToUserInformationAsync(user);
        }

        public async Task<UserInformationBlo> AuthWithLogin(string login, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(p => p.Login == login && p.Password == password);
            if (user == null)
                throw new NotFoundException($"Пользователь с логином {login} не найден");
            return await ConvertToUserInformationAsync(user);
        }
        public async Task<UserInformationBlo> AuthWithPhone(string numberPrefix, string number, string password)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(p => p.PhoneNumberPrefix == numberPrefix && p.PhoneNumber == number && p.Password == password);
            if (user == null)
                throw new NotFoundException($"Пользователь с телефоном {numberPrefix}{number} не найден");
            return await ConvertToUserInformationAsync(user);
        }

        public async Task<bool> DoesExist(string numberPrefix, string number)
        {
          bool result = await _context.Users.AnyAsync(y => y.PhoneNumber == number && y.PhoneNumberPrefix == numberPrefix);
            return result;
        }

        public async Task<UserInformationBlo> Get(int userId)
        {
          UserRto user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user == null)
            {
                throw new NotFoundException("Пользователь не найден");
                
            }
            UserInformationBlo userInfoBlo = await ConvertToUserInformationAsync(user);
            return userInfoBlo;

        }

        public async Task<UserInformationBlo> RegisterWithPhone(string numberPrefix, string number, string password)
        {
            bool result = await _context.Users.AnyAsync(y => y.PhoneNumber == number && y.PhoneNumberPrefix == numberPrefix);
            if (result == true)
            
                throw new BadRequestExceptions("Такой пользователь уже существует");
            UserRto user = new UserRto()
            {
                Password = password,
                PhoneNumber = number,
                PhoneNumberPrefix = numberPrefix

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            UserInformationBlo UserInfBlo = await ConvertToUserInformationAsync(user);
            return UserInfBlo;
        }

        public async Task<UserInformationBlo> Update(string numberPrefix, string number, string password, UserUpdateBlo userUpdateBlo)
        {
            UserRto user = await _context.Users.FirstOrDefaultAsync(y => y.PhoneNumber == number && y.PhoneNumberPrefix == numberPrefix);
            if (user == null)
            
                throw new BadRequestExceptions("Такого пользоваетля нет");
                user.Password = userUpdateBlo.Password;
                user.Login = userUpdateBlo.Login;
                user.Id = userUpdateBlo.Id;
                user.Email = userUpdateBlo.Email;
                user.AvatarUrl = userUpdateBlo.AvatarUrl;
                user.LastName = userUpdateBlo.LastName;
                user.Patronymic = userUpdateBlo.Patronymic;
                user.IsBoy = userUpdateBlo.IsBoy;
                user.PhoneNumber = userUpdateBlo.PhoneNumber;
                user.PhoneNumberPrefix = userUpdateBlo.PhoneNumberPrefix;
                user.Birthday = userUpdateBlo.Birthday;
                user.FirstName = userUpdateBlo.FirstName;

                UserInformationBlo UserInfBlo = await ConvertToUserInformationAsync(user);
                return UserInfBlo;
        }


        private async Task<UserInformationBlo> ConvertToUserInformationAsync(UserRto userRto)
        {
            if (userRto == null) throw new ArgumentNullException(nameof(userRto));

            UserInformationBlo userInformationBlo = _mapper.Map<UserInformationBlo>(userRto);

            return userInformationBlo;
        }
    }
}
