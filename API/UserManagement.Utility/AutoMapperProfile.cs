using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UserManage.DTO;
using UserManage.Model;

namespace UserManage.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            #endregion Rol


            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion Menu

            #region MenuRol
            CreateMap<MenuRol, MenuRolDTO>()
                .ForMember(dest => dest.IdRol, opt => opt.MapFrom(src => src.IdRol))
                .ReverseMap();
            #endregion MenuRol


            #region User
            CreateMap<User,UserDTO>()
            .ForMember(destino =>
            destino.RolDescription,
            opt=> opt.MapFrom(origen=>origen.IdRolNavigation.Name)
            )
            .ForMember(destino=>destino.IsActive,
            opt=>opt.MapFrom(origen=>origen.IsActive==true? 1:0)
            );
            CreateMap<User, SesionDTO>()
            .ForMember(destino =>
             destino.RolDescription,
             opt => opt.MapFrom(origen => origen.IdRolNavigation.Name)
           );


            CreateMap<UserDTO, User>()
                .ForMember(destino =>
            destino.IdRolNavigation,
            opt => opt.Ignore() 
            )
                .ForMember(destino => destino.IsActive,
            opt => opt.MapFrom(origen => origen.IsActive == 1 ? true:false)
            );



            #endregion User



        }

    }
}
