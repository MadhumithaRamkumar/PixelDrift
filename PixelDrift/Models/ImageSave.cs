//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PixelDrift.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImageSave
    {
        public int ImageSaveId { get; set; }
        public string User_Id { get; set; }
        public string FileName { get; set; }
        public Nullable<int> ImageId { get; set; }
    
        public virtual User_login User_login { get; set; }
    }
}