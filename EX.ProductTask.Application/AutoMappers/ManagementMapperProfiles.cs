using Application.Dtos.Auth.Audit;
using Application.Dtos.Auth.Users;
using Application.Dtos.roles;
using Application.Dtos.Users;
using AutoMapper;
using Core.Common.Dto;
using Core.Entities.Management;
using Microsoft.AspNetCore.Identity;

namespace Application.Automappers;
public class ManagementMapperProfiles : Profile
{
    public ManagementMapperProfiles()
    {
        // users
        CreateMap<IdentityUser, UserListDto>();
         CreateMap<IdentityUser, BaseListDto>() ;

        CreateMap<UserLoginDto, IdentityUser>();

        CreateMap<UserRegisterDto, IdentityUser>().ReverseMap();
        CreateMap<UserEditDto, IdentityUser>();
        CreateMap<IdentityUser, UserGetDto>();


		// Roles 
		CreateMap<IdentityRole, RoleListDto>();
		CreateMap<IdentityRole, BaseListDto>();

		CreateMap<RoleRegisterDto, IdentityRole>().ReverseMap();
		CreateMap<IdentityRole, RoleListEditDto>();


		CreateMap<Audit, AuditGetDto>();




	}
}
