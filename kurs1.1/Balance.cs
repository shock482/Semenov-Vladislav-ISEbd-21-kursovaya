namespace kurs1._1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Balance")]
    public partial class Balance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WalletID { get; set; }

        [Column("Balance on wallet")]
        public int Balance_on_wallet { get; set; }
    }
}
