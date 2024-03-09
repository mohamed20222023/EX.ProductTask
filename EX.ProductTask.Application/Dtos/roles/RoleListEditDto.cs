using System;
using Core.Common;

namespace Application.Dtos.roles
{
    public class RoleListEditDto  
    {
         public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEditDate { get; set; }
       
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Display { get; set; }
        public string DisplayAr { get; set; }


    }
}