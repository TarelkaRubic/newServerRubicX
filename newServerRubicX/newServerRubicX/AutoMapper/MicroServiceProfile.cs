using AutoMapper;
using newServerRubicX.BusinessLogic.Core.Models;
using newServerRubicX.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newServerRubicX.AutoMapper
{
    public class MicroServiceProfile : Profile
    {
        public MicroServiceProfile()
        {
            CreateMap<UserInformationBlo, UserInformationDto>();
            CreateMap<UserUpdateBlo, UserUpdateBlo>();
        }
    }
    
}
