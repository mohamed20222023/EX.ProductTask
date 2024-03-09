// using Core.Interfaces;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Filters;
// using Microsoft.Extensions.Localization;
// using Core.Entities.Management;
// using WebApi.Resources;

// namespace Users.API.ActionFilter
// {
//     public class ValidateRoleExist : IAsyncActionFilter
//     {
//         private readonly IRepositoryApp<Role> _roleRepo;
//         private readonly IStringLocalizer<Resource> _localizer;
//         public ValidateRoleExist(
//         IRepositoryApp<Role> roleRepo,
//             IStringLocalizer<Resource> localizer)
//         {
//             _localizer = localizer;
//             _roleRepo = roleRepo;
//         }

//         public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//         {
//             var role = context.ActionArguments.Values.FirstOrDefault() as RoleRegisterDto;
//             var entity = (await _roleRepo.GetAllAsync(x => x.Name == role.Name || x.NameEn == role.NameEn || x.NameAr == role.NameAr)).FirstOrDefault();
//             if (entity != null)
//             {
//                 context.Result = new BadRequestObjectResult(_localizer["rolefound"].Value);
//                 return;
//             }

//             await next();
//         }


//     }
// }




