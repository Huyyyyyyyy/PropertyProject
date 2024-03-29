//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PropertyProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Feedbacks = new HashSet<Feedback>();
            this.PropertyPosts = new HashSet<PropertyPost>();
        }
    
        public string idCustomer { get; set; }
        public string emailCus { get; set; }
        public string introduceCode { get; set; }
        public string phoneCus { get; set; }
        public Nullable<System.DateTime> birthCus { get; set; }
        public string locationCus { get; set; }
        public int availableProperty { get; set; }
        public int expiredProperty { get; set; }
        public int pendingProperty { get; set; }
        public decimal balance { get; set; }
        public string avatarCus { get; set; }
        public string nameCus { get; set; }
        public string genderCus { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Feedback Feedback { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyPost> PropertyPosts { get; set; }
    }
}
