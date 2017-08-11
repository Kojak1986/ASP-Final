namespace Comp2007_Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Colour
    {
        public Colour()
        {

        }

        [Key]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public string ColourId { get; set; }

        [Required]
        [StringLength(250)]
        [Display(Name ="Colour")]
        public string Name { get; set; }

       // [StringLength(128)]
       // [Display(Name = "Type")]
        //public string Type { get; set; }

        [Display(Name = "Create Date")]
        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Edit Date")]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "Items")]
        [InverseProperty("Colour")]
        public virtual ICollection<Order> Items { get; set; } = new HashSet<Order>();

        public override string ToString()
        {
            return String.Format("{0}", Name);
        }
    }
}
