namespace emdo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        public int id { get; set; }

        [StringLength(150)]
        public string title { get; set; }

        [StringLength(250)]
        public string bref { get; set; }

        public string desc { get; set; }

        public DateTime? date { get; set; }

        [StringLength(150)]
        public string photo { get; set; }

        public int? cat_id { get; set; }
        [Display(Name ="catlog")]
        public int? user_id { get; set; }

        public virtual catalog catalog { get; set; }

        public virtual user user { get; set; }
    }
}
