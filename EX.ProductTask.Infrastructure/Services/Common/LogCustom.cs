using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Management;
using Core.Enums;
using Core.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;

namespace Infrastructure.Services.Common;
public class LogCustom : ILogCustom
{
    private readonly ILogger<LogCustom> _logger;
    private readonly IHttpContextAccessor _accssor;
    private readonly UserManager<IdentityUser> user;
     public LogCustom(
        ILogger<LogCustom> logger,
        IHttpContextAccessor accssor,
        UserManager<IdentityUser> User
    )
    {

        _logger = logger;
        _accssor = accssor;
        user = User;
    }
    public void LogInSuccess(string UserName)
    {
       var httpContext = _accssor.HttpContext;
        var request = httpContext.Request;
        LogContext.PushProperty("UserName", UserName);
        LogContext.PushProperty("UserFullName", UserName);
        LogContext.PushProperty("TableName", "Auth");
        LogContext.PushProperty("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
        LogContext.PushProperty("ServerIP", httpContext.Connection.LocalIpAddress.ToString());
        LogContext.PushProperty("StatusCode",httpContext.Response.StatusCode);
        LogContext.PushProperty("State", AuditType.login.ToString());
        LogContext.PushProperty("IsShowUser", true);
         LogContext.PushProperty("IsLogin", true);
        LogContext.PushProperty("IsLogout", false);
        LogContext.PushProperty("QueryString", request.QueryString);
        Log.Information("successLogin");
   }
    public void SignUpSuccess(string UserName)
    {
       var httpContext = _accssor.HttpContext;
        var request = httpContext.Request;
        LogContext.PushProperty("UserName", UserName);
        LogContext.PushProperty("UserFullName", UserName);
        LogContext.PushProperty("TableName", "Auth");
        LogContext.PushProperty("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
        LogContext.PushProperty("ServerIP", httpContext.Connection.LocalIpAddress.ToString());
        LogContext.PushProperty("StatusCode",httpContext.Response.StatusCode);
        LogContext.PushProperty("State", AuditType.signUp.ToString());
        LogContext.PushProperty("IsShowUser", true);
         LogContext.PushProperty("IsLogin", false);
        LogContext.PushProperty("IsLogout", false);
        LogContext.PushProperty("QueryString", request.QueryString);
        Log.Information("successRegister");
   }
    public void LogOutSuccess(string UserName, string controllerName, string actionName)
    {
        var httpContext = _accssor.HttpContext;
        var request = httpContext.Request;
        LogContext.PushProperty("UserName", UserName);
        LogContext.PushProperty("UserFullName", UserName);
          LogContext.PushProperty("TableName", "Auth");
        LogContext.PushProperty("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
        LogContext.PushProperty("ServerIP", httpContext.Connection.LocalIpAddress.ToString());
        LogContext.PushProperty("StatusCode",httpContext.Response.StatusCode);
        LogContext.PushProperty("State", AuditType.logout.ToString());
        LogContext.PushProperty("QueryString", request.QueryString);
        LogContext.PushProperty("IsShowUser", true);
        LogContext.PushProperty("IsLogin", false);
        LogContext.PushProperty("IsLogout", true);

        Log.Information("successLogout");
    }
    public void LogInFail(string UserName,string password)
    {
   var httpContext = _accssor.HttpContext;
        var request = httpContext.Request;
        LogContext.PushProperty("UserName", UserName);
        LogContext.PushProperty("UserFullName", UserName);
        LogContext.PushProperty("TableName", "Auth");
        //LogContext.PushProperty("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
        //LogContext.PushProperty("ServerIP", httpContext.Connection.LocalIpAddress.ToString());
        LogContext.PushProperty("StatusCode",httpContext.Response.StatusCode);
        LogContext.PushProperty("State", AuditType.login.ToString());
        LogContext.PushProperty("IsShowUser", true);
         LogContext.PushProperty("IsLogin", true);
        LogContext.PushProperty("IsLogout", false);
        LogContext.PushProperty("QueryString", request.QueryString);
        LogContext.PushProperty("NewValues",  JsonConvert.SerializeObject(new {userName=UserName,password=password}));
        Log.Warning("failLogin");
    }
  
    public void Info<T>(string message,object queryString,AuditType State=AuditType.get,object oldValues=null,object newValues=null)
    {
        var httpContext = _accssor.HttpContext;
        var claimsIdentity = httpContext.User;
        var userName = claimsIdentity == null ?  "system" : claimsIdentity.Identities.FirstOrDefault().Name;
        //var UserFullName = claimsIdentity==null||claimsIdentity.Identities.FirstOrDefault().Claims.Count()<=0  ? "system" : claimsIdentity.Identities.FirstOrDefault().Claims.FirstOrDefault(a => a.Type == ClaimTypes.Surname).Value;
        var request = httpContext.Request;
        LogContext.PushProperty("UserName", userName);
        LogContext.PushProperty("TableName", typeof(T).Name);
        LogContext.PushProperty("State", State.ToString());
        //LogContext.PushProperty("UserFullName", UserFullName);
        //LogContext.PushProperty("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
        //LogContext.PushProperty("ServerIP", httpContext.Connection.LocalIpAddress.ToString());
        LogContext.PushProperty("StatusCode", httpContext.Response.StatusCode);
        LogContext.PushProperty("IsShowUser", true);
              LogContext.PushProperty("IsLogin", false);
        LogContext.PushProperty("IsLogout", false);
        LogContext.PushProperty("QueryString", JsonConvert.SerializeObject(queryString));
           LogContext.PushProperty("OldValues",oldValues!=null? JsonConvert.SerializeObject(oldValues):oldValues);
        LogContext.PushProperty("NewValues", newValues!=null? JsonConvert.SerializeObject(newValues):newValues);
        Log.Information(message);
     }
     
  
    public void AppStartClose(string masg,bool IsStart)
    {
       var key = masg;
        LogContext.PushProperty("UserName", "system");
        LogContext.PushProperty("MessageAr", masg);
        LogContext.PushProperty("State", IsStart? AuditType.appStart.ToString():AuditType.appClose.ToString());
        LogContext.PushProperty("StatusCode", 200);
        LogContext.PushProperty("IsShowUser", false);
        LogContext.PushProperty("IsLogin", false);
        LogContext.PushProperty("IsLogout", false);

        Log.Information(masg);
    }

    public void GlobalError(string masg, int statusCode)
    {
        var httpContext = _accssor.HttpContext;
        var claimsIdentity = httpContext.User;
        var userName = claimsIdentity.Claims.Count()<=0 ? "system" : claimsIdentity.Identities.FirstOrDefault().Name;
        var UserFullName = claimsIdentity.Claims.Count()<=0? "system" : claimsIdentity.Identities.FirstOrDefault().Claims.FirstOrDefault(a => a.Type == ClaimTypes.Surname).Value;
        var request = httpContext.Request;
        LogContext.PushProperty("UserName", userName);
         LogContext.PushProperty("UserFullName", UserFullName);
        LogContext.PushProperty("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
        LogContext.PushProperty("ServerIP", httpContext.Connection.LocalIpAddress.ToString());
        LogContext.PushProperty("StatusCode",statusCode);
        LogContext.PushProperty("IsShowUser", false);
         LogContext.PushProperty("IsLogin", false);
        LogContext.PushProperty("IsLogout", false);
        LogContext.PushProperty("QueryString", request.QueryString);
        Log.Warning(masg);
    }

    public void Info(Audit audit)
    {
        var httpContext = _accssor.HttpContext;
        var claimsIdentity = httpContext.User;
        var userName = claimsIdentity == null ? "system" : claimsIdentity.Identities.FirstOrDefault().Name;
        var UserFullName = claimsIdentity == null|| !claimsIdentity.Identities.FirstOrDefault().Claims.Any() ? "system" : claimsIdentity.Identities.FirstOrDefault().Claims.FirstOrDefault(a => a.Type == ClaimTypes.Surname).Value;
        var request = httpContext.Request;
        LogContext.PushProperty("UserName", userName);
        LogContext.PushProperty("RowClientId", audit.RowClientId);
        LogContext.PushProperty("TableName", audit.TableName);
        LogContext.PushProperty("State", audit.State);
        LogContext.PushProperty("OldValues", audit.OldValues);
        LogContext.PushProperty("NewValues", audit.NewValues);
        LogContext.PushProperty("PrimaryKeyObj", audit.PrimaryKeyObj);
        LogContext.PushProperty("UserFullName", UserFullName);
        LogContext.PushProperty("ClientIP", httpContext.Connection.RemoteIpAddress.ToString());
        LogContext.PushProperty("ServerIP", httpContext.Connection.LocalIpAddress.ToString());
        LogContext.PushProperty("StatusCode", httpContext.Response.StatusCode);
        LogContext.PushProperty("IsShowUser", true);
         LogContext.PushProperty("IsLogin", false);
        LogContext.PushProperty("IsLogout", false);
        LogContext.PushProperty("QueryString", request.QueryString);
       Log.Information(audit.State);
   }
   

     
}

