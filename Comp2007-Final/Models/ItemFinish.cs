namespace Comp2007_Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemFinish")]
    public partial class ItemFinish
    {
        [Key]
        public string FinishId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name ="Finish")]
        public string Name { get; set; }

        [Display(Name = "Items")]
        [InverseProperty("ItemFinish")]
        public virtual ICollection<Order> Items { get; set; } = new HashSet<Order>();
    }
}
