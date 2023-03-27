using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDevAPI.Db.Models
{
    [Keyless]
    [NotMapped]
    public class PlayerHand
    {
        public Guid PlayerId { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
