//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PastebookEntityLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class NOTIFICATION
    {
        public int ID { get; set; }
        public int RECEIVER_ID { get; set; }
        public string NOTIF_TYPE { get; set; }
        public int SENDER_ID { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public Nullable<int> COMMENT_ID { get; set; }
        public Nullable<int> POST_ID { get; set; }
        public string SEEN { get; set; }
    
        public virtual COMMENT COMMENT { get; set; }
        public virtual POST POST { get; set; }
        public virtual USER USER { get; set; }
        public virtual USER USER1 { get; set; }
    }
}
