using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace secondassesment
{
    [Table("family_table")]
    class Family
    {
        [Column("Father_Name")]
        public string fathername { get; set; }

        [Column("Mother_Name")]
        public string mothername { get; set; }

        [Column("Child_Name")]
        [MaxLength(2)]
        public string Address { get; set; }
        [Column("Children_Name")]
        public string children { get; set; }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }



    }
}