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
    
    public partial class Admin
    {
        public string idAd { get; set; }
        public string emailAd { get; set; }
        public string nameAd { get; set; }
        public string PhoneAd { get; set; }
        public string avatarAd { get; set; }
        public string genderAd { get; set; }
    
        public virtual Account Account { get; set; }
    }
}
