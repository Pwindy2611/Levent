//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Levent.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderPro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderPro()
        {
            this.OrderDetail = new HashSet<OrderDetail>();
        }
    
        public int ID_OrderPro { get; set; }
        public Nullable<int> ID_User { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual User1 User1 { get; set; }
    }
}
