//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Project
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        public int Review_Id { get; set; }
        public int Review_Rating { get; set; }
        public string Review_Text { get; set; }
        public int Customer_Id { get; set; }
        public System.DateTime Review_Date { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
