using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Common;
     public class BaseEntity : BaseId,IBaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public string UserCreatedName { get; set; }
        public string UserLastEditName { get; set; }
        public bool IsDeleted { get; set; }
         public Guid? ClientId { get; set; }  // forign key for index
 }

