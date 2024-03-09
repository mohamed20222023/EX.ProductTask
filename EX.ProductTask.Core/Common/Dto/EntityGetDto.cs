using System;

namespace Core.Common.Dto
{
    public class EntityGetDto:EntityRegisterDto
    {
            public Guid Id { get; set; }

         public DateTime CreatedDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public  string CreatedUserName { get; set; }
        public  string LastEditUserName { get; set; }
        
    }
}