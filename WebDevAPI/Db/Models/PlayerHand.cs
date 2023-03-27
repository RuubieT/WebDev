using System.ComponentModel.DataAnnotations;

namespace WebDevAPI.Db.Models
{
    public class PlayerHand
    {
        [Key] public Guid PlayerHandId { get; set; }

        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
