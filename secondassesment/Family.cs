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
    [Table("family_table")]
    class Family
    {
        [Column("Father_Name")]
        public string fathername { get; set; }

        [Column("Mother_Name")]
        public string mothername { get; set; }

        [Column("Address")]
        
        public string Address { get; set; }
       

        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        [OneToMany]
        public List<childmodel> Childrens { get; set; }

    }
}