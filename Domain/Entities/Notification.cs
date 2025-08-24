using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hahn.Domain.Entities
{
    [Index(nameof(notifiedId))] 
    public class Notification
    {
        [Key]
        public int id { get; private set; }

        public int? notifierId { get; private set; }
        [ForeignKey(nameof(notifierId))]
        public User? Notifier { get; private set; }

        public int notifiedId { get; private set; }
        [ForeignKey(nameof(notifiedId))]
        public User Notified { get; private set; } = null!;

        [Required]
        public string subject { get; private set; } = string.Empty;

        [Required]
        public string content { get; private set; } = string.Empty;

        public DateTime time { get; private set; } = DateTime.UtcNow;

        public bool seen { get; private set; } = false;


        private Notification() { }

        public static Notification Create(
            int notifiedId, 
            string subject, 
            string content, 
            int? notifierId = null)
        {
            return new Notification
            {
                notifiedId = notifiedId,
                subject = subject,
                content = content,
                notifierId = notifierId,
                time = DateTime.UtcNow,
                seen = false
            };
        }

        public void MarkAsSeen()
        {
            seen = true;
        }
    }

    
}
