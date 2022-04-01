using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace secondassesment
{
    [Activity(Label = "addfamilyactivity", Theme = "@style/AppTheme")]
    public class addfamilyactivity : AppCompatActivity
    {
        AndroidX.AppCompat.Widget.Toolbar toolbar;
        TextInputEditText fatheredittext, motheredittext, addressedittext;
        ScrollView scrollview;
        LinearLayout linearLayout;
        EditText view;
        EditText ed;
        List<EditText> allEds = new List<EditText>();
        LinearLayout.LayoutParams layoutParams;
        Button b, save;
        Familydatabase DB;
        int pos = 0;


        List<string> listofchild = new List<string>();
        string combinedString;
        List<int> ids = new List<int>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.addfamilyactivity);
            scrollview = FindViewById<ScrollView>(Resource.Id.scrollable);
            save = FindViewById<Button>(Resource.Id.savebutton);
            linearLayout = FindViewById<LinearLayout>(Resource.Id.gamehistory);
            toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.tool);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetShowHideAnimationEnabled(true);
            fatheredittext = FindViewById<TextInputEditText>(Resource.Id.fathernamedittext);
            motheredittext = FindViewById<TextInputEditText>(Resource.Id.mothernamedittext);
            addressedittext = FindViewById<TextInputEditText>(Resource.Id.addressedittext);
            b = FindViewById<Button>(Resource.Id.Button01);
            DB = new Familydatabase();
            DB.FamilyTable();
            layoutParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.WrapContent, LinearLayout.LayoutParams.WrapContent);
            checkIntent();
            clickevents();

        }

        private void clickevents()
        {
            b.Click += B_Click;
            save.Click += Save_Click;
        }

        private void checkIntent()
        {
            if (Intent.HasExtra("MyData"))
            {
                save.Text = "Update";
                pos = Intent.GetIntExtra("MyData", 0);
                Family fam = DB.GetByFamilyId(pos);
                fatheredittext.Text = fam.fathername;
                motheredittext.Text = fam.mothername;

               


                addressedittext.Text = fam.Address;
                //childeeess.AddRange(fam.children.ToList());

                List<string> childnames = new List<string>();
                childnames = fam.children.Split(',').ToList();
                for (int i = 0; i < childnames.Count; i++)
                {
                    EditText view = new EditText(this);
                    view.Id = View.GenerateViewId();
                    view.Hint = "Enter Child Name";
                    view.Text = childnames[i];
                    linearLayout.AddView(view);
                    allEds.Add(view);
                }

                //listofchild.AddRange(fam.children.Split(',').ToList());


            }
        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Family family = new Family();
            string father, mother, address;
            father = fatheredittext.Text;
            mother = motheredittext.Text;
            address = addressedittext.Text;
            family.fathername = father;
            family.mothername = mother;
            family.Address = address;

            List<string> childs = new List<string>();

            for (int i = 0; i < allEds.Count; i++)
            {
                if (allEds[i] != null)
                {
                    string childtext = allEds[i].Text.ToString();
                    childs.Add(childtext);


                }
                combinedString = string.Join(",", childs);

            }
            family.children = combinedString;
            if (save.Text == "Update")
            {
                upDateClicked(family);
            }
            else
            {
                saveClicked(family);
            }
        }

        private void upDateClicked(Family family)
        {
            bool checkinsert = DB.updatefamily(family, pos);

            if (checkinsert == true)
            {
                
                Toast.MakeText(this, "Data Updated Succesfully", ToastLength.Short).Show();

            }
            else
            {

                Toast.MakeText(this, "No action performed", ToastLength.Short).Show();

            }
        }

        private void saveClicked(Family family)
        {




            bool checkinsert = DB.InstertFamily(family);

            if (checkinsert == true)
            {

                Toast.MakeText(this, "Data Inserted Succesfully", ToastLength.Short).Show();
                

            }
            else
            {

                Toast.MakeText(this, "No action performed", ToastLength.Short).Show();

            }
        }

        private void B_Click(object sender, EventArgs e)
        {
            view = new EditText(this);
            allEds.Add(view);

            view.Id = View.GenerateViewId();
            view.Hint = "Enter Child Name";
            linearLayout.AddView(view);
        }
    }
}