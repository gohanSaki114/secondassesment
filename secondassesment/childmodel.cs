using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace secondassesment
{
    [Table("child_table")]
    class childmodel
    {
        [Column("F_id"), ForeignKey(typeof(Family))]
        public int familyId { get; set; }
        [PrimaryKey, AutoIncrement, Column("C_id")]
        public int childId { get; set; }

        [Column("childname")]
        public string childname { get; set; }

    }
}