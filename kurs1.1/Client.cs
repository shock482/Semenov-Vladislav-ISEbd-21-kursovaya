namespace kurs1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientID { get; set; }

        [Column("Full name")]
        [Required]
        [StringLength(50)]
        public string Full_name { get; set; }

        public int? Telephone { get; set; }

        [Column("E-mail")]
        [StringLength(50)]
        public string E_mail { get; set; }
    }
}
