//using System;
//using System.Text;
//using Core.Entities.Management;
//using Infrastructure.Data;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;

//namespace Infrastructure.Services
//{
//    public static class AuthenticationServices
//    {
//        public static void AddAuthenticationServices(this IServiceCollection services, string ConfigurationToken)
//        {

//            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
//                  { //Helper functions for configuring identity services.
//                      opt.Password.RequireDigit = false;
//                      opt.Password.RequiredLength = 1;
//                      opt.Password.RequireNonAlphanumeric = false;
//                      opt.Password.RequireUppercase = false;
//                      opt.Password.RequireLowercase = false;
//                  });
//            // 2- IdentityBuilder config
//            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
//            builder.AddEntityFrameworkStores<AppDbContext>();
//            builder.AddRoleValidator<RoleValidator<Role>>();
//            builder.AddRoles<Role>();
//            builder.AddRoleManager<RoleManager<Role>>();
//            builder.AddSignInManager<SignInManager<User>>();

//            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            .AddJwtBearer(Options =>
//            {
//                Options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ConfigurationToken)),
//                    ValidateIssuer = false,
//                    ValidateAudience = false

//                };
//            });

//		}
//    }
//}