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
    
    public partial class ImageProperty
    {
        public string imageLink { get; set; }
        public string idPost { get; set; }
    
        public virtual PropertyPost PropertyPost { get; set; }
    }
}
