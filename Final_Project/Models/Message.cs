﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final_Project.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Content { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        // a message can belong to one group
        // Single group can have many messages
        [ForeignKey("Group")]
        public int Id { get; set; }
        public virtual Group Group { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class MessageDto
    {
        public int MessageId { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }

    }
}