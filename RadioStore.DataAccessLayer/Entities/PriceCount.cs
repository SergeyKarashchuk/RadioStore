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
    
    public partial class PriceCount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PriceCount()
        {
            this.PriceProducts = new HashSet<PriceProduct>();
        }
    
        public int PriceCountId { get; set; }
        public int ProductCount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceProduct> PriceProducts { get; set; }
    }
}