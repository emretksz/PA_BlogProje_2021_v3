//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PA_BlogProject_2021.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BlogDetails
    {
        public int BlogId { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string AuthorJob { get; set; }
        public string AuthorDescription { get; set; }
        public string AuthorImageURL { get; set; }
        public string TwitterURL { get; set; }
        public string LinkedinURL { get; set; }
    
        public virtual Blogs Blogs { get; set; }
    }
}