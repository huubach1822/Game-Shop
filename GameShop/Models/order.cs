//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.order_product = new HashSet<order_product>();
        }
    
        public Nullable<System.DateTime> create_at { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<System.DateTime> update_at { get; set; }
        public Nullable<bool> deleted { get; set; }
        public int users_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_product> order_product { get; set; }
        public virtual user user { get; set; }
    }
}
