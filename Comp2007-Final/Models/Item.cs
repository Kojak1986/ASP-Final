namespace Comp2007_Final.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string ItemId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Display(Name = "Is A Gift")]
        public bool IsGift { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Colours")]
        [InverseProperty("Item")]
        public virtual ICollection<Order> Colours { get; set; } = new HashSet<Order>();

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}
