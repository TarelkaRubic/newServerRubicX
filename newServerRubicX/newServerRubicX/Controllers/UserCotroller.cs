using Microsoft.AspNetCore.Mvc;
using newServerRubicX.BusinessLogic.Core.Models;

using newServerRubicX.BusinessLogic.Services;
using newServerRubicX.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using newServerRubicX.BusinessLogic.Core.Interfaces;

namespace newServerRubicX.Controllers
{
    [Controller]
    [Route("/api/[controller]")]
    public class UserCotroller : ControllerBase
    {
        private readonly IUserServices  _userService;
        private readonly IMapper _mapper;
        public UserCotroller(IUserServices userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
  
        }
        
        [HttpPost("registration")]
        public async Task<ActionResult<UserInformationDto>> Register(UserIdentityDto userIdentityDto)
        {
            UserInformationBlo userInformationBlo = await _userService.RegisterWithPhone(userIdentityDto.numberPrefix, userIdentityDto.number, userIdentityDto.password);
            return await ConvertToUserInformationAsync(userInformationBlo);
        }
        private async Task<UserInformationDto> ConvertToUserInformationAsync(UserInformationBlo userInformationBlo)
        {
            if (userInformationBlo == null) throw new ArgumentNullException(nameof(UserInformationBlo));

            UserInformationDto userInformationDto = _mapper.Map<UserInformationDto>(userInformationBlo);

            return userInformationDto;
        }
    }
}

