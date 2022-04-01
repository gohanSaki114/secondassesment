using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.RecyclerView.Widget;
using Google.Android.Material.FloatingActionButton;
using System;
using System.Collections.Generic;

namespace secondassesment
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Toolbar toolbar;
        FloatingActionButton floatingbutton;
        RecyclerView myRecycleView;
        Familydatabase Db;
        RecyclerView.LayoutManager myLayoutmanager;
        private Familyadapter sadapter;
        List<Family> data;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RecyclerView recycler;
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            toolbar = FindViewById<Toolbar>(Resource.Id.tool);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetShowHideAnimationEnabled(true);
            Db = new Familydatabase();
            Db.FamilyTable();
            data = Db.ReadFamily();
            myRecycleView = FindViewById<RecyclerView>(Resource.Id.recycelervv);
            floatingbutton = FindViewById<FloatingActionButton>(Resource.Id.floatingbutton);
            myLayoutmanager = new LinearLayoutManager(this);
            myRecycleView.SetLayoutManager(myLayoutmanager);
            sadapter = new Familyadapter(data, this);
            myRecycleView.SetAdapter(sadapter);
            sadapter.deletepos += Sadapter_deletepos;
            floatingbutton.Click += Floatingbutton_Click;
            sadapter.ItemLongClick += Sadapter_ItemLongClick;

        }

        private void Sadapter_ItemLongClick(object sender, FamilyadapterClickEventArgs e)
        {
            var id = (sender as Familyadapter).data[e.Position].Id;
            Intent intent = new Intent(this,typeof(addfamilyactivity));
            int pos = e.Position;
            intent.PutExtra("MyData", id);
           
            StartActivity(intent);

        }

        public override bool OnSupportNavigateUp()
        {
            OnBackPressed();
            return true;
        }

        private void Floatingbutton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(addfamilyactivity));
            StartActivity(intent);
        }

        private void Sadapter_deletepos(object sender, FamilyadapterClickEventArgs e)
        {

            int pos = e.Position;
            int id = data[pos].Id;
           
            
            Family family = data[pos];
            bool succes = Db.DeleteFamily(family);
            if (succes)
            {
                Android.Widget.Toast.MakeText(this, "Items deleted successfully", Android.Widget.ToastLength.Short).Show();
                data.RemoveAt(pos);
                sadapter.NotifyItemRemoved(pos);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}