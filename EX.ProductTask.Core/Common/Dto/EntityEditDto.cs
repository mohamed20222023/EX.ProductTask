using System;

namespace Core.Common.Dto
{
    public class EntityEditDto:BaseId
    {
         public string Name { get; set; }
        public string Description { get; set; } 
    }
}