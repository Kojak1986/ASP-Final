namespace Comp2007_Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string OrderId { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Item")]
        public string ItemId { get; set; }

        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Colour")]
        public string ColourId { get; set; }

        [ForeignKey("ColourId")]
        public virtual Colour Colour { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;

        public override string ToString()
        {
            return String.Format("{0} - {1}", Item.ToString(), Colour.ToString());
        }
    }
}
