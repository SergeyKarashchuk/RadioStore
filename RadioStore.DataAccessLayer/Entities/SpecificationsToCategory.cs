//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RadioStore.DataAccessLayer.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class SpecificationsToCategory
    {
        public int SpecificationTypeId { get; set; }
        public int CategoryId { get; set; }
        public int SpecificationToCategoryId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual SpecificationType SpecificationType { get; set; }
    }
}
