using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Environment = System.Environment;

namespace secondassesment
{
    class Familydatabase
    {

        public static string DBname = "SQLite.db3";
        public static string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DBname);

        SQLiteConnection sqliteConnection;

        public Familydatabase()
        {
            try
            {
                Console.WriteLine(DBPath);
                sqliteConnection = new SQLiteConnection(DBPath);
                Console.WriteLine("Succefully Database Created!....");



            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Exception:" + ex);

            }
        }

        public void FamilyTable()
        {
            try
            {
                var created = sqliteConnection.CreateTable<Family>();
                Console.WriteLine("Succefully Table Created!....");

            }

            catch (Exception ex)
            {
                Console.WriteLine("Database Exception:" + ex);

            }

        }

        public bool InstertFamily(Family family)
        {


            long result = sqliteConnection.Insert(family);



            if (result == -1)
            {

                return false;
            }

            else
            {
                Console.WriteLine("Succefully Inserted Data ");
                return true;
            }


        }
        public bool updatefamily(Family family,int id)
        {
            family.Id = id;
            long result = sqliteConnection.Update(family);


            if (result == 1)
            {


                Console.WriteLine("Succefully Updated Data ");
                return true;
            }
            else
            {
                Console.WriteLine("Not any action perform ");
                return false;

            }


        }
        public List<Family> ReadFamily()
        {
            try
            {
                var FamilyDetails = sqliteConnection.Table<Family>().ToList();
                return FamilyDetails;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Database Exception:" + ex);
                return null;
            }

        }

        public Family GetByFamilyId(int familyid)
        {
            var userId = ReadFamily().Where(u => u.Id == familyid).FirstOrDefault();
           
            return userId;

        }

        public bool DeleteFamily(Family family)
        {


            long result = sqliteConnection.Delete(family);
            if (result == -1)
            {

                return false;
            }

            else
            {
                Console.WriteLine("Succefully Deleted Data ");
                return true;
            }


        }








    }
    }
