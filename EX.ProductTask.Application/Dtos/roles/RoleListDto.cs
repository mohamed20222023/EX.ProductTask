using System;
using Core.Common;

namespace Application.Dtos.roles;
    public class RoleListDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }


    }
