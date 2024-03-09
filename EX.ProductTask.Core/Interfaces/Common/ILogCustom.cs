using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Management;
using Core.Enums;

namespace Core.Interfaces.Common;
public interface ILogCustom
{
    void LogInSuccess(string UserName);
    void SignUpSuccess(string UserName);
   void LogInFail(string UserName,string password);
    void LogOutSuccess(string UserName, string controllerName, string actionName);
    void Info(Audit audit);
    void Info<T>(string key,object queryString,AuditType State=AuditType.get,object oldValues=null,object newValues=null);
      void AppStartClose(string masg,bool IsStart);
    void GlobalError(string masg,int statusCode);
}
