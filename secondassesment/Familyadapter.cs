
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;
using System.Collections.Generic;
using System.Linq;

namespace secondassesment
{
    internal class Familyadapter : RecyclerView.Adapter
    {
        public event EventHandler<FamilyadapterClickEventArgs> ItemClick;
        public event EventHandler<FamilyadapterClickEventArgs> ItemLongClick;
        public event EventHandler<FamilyadapterClickEventArgs> deletepos;
        Familydatabase Db;
        public List<Family> data;
        private MainActivity mainActivity;



        public Familyadapter(List<Family> data, MainActivity mainActivity)
        {
            this.data = data;
            this.mainActivity = mainActivity;
        }



        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {


            View itemView = null;
            var id = Resource.Layout.familyrowitem;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new FamilyadapterViewHolder(itemView, OnClick, OnLongClick, deletepressed);
            return vh;
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = data[position];


            var holder = viewHolder as FamilyadapterViewHolder;
            holder.fatherTextView.Text = item.fathername;
            holder.motheTextView.Text = item.mothername;
            holder.AddressTextView.Text = item.Address;
            holder.childTextView.Text = string.Join(", ", item.Childrens.Select(x => x.childname).ToList());

        }

        public override int ItemCount => data.Count;
        void deletepressed(FamilyadapterClickEventArgs args) => deletepos?.Invoke(this, args);
        void OnClick(FamilyadapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(FamilyadapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class FamilyadapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView fatherTextView, motheTextView, AddressTextView, childTextView;
        public ImageButton delete;

        public FamilyadapterViewHolder(View itemView, Action<FamilyadapterClickEventArgs> clickListener,
                            Action<FamilyadapterClickEventArgs> longClickListener, Action<FamilyadapterClickEventArgs> deletepressed) : base(itemView)
        {
            fatherTextView = itemView.FindViewById<TextView>(Resource.Id.fathername);
            motheTextView = itemView.FindViewById<TextView>(Resource.Id.mothername);
            AddressTextView = itemView.FindViewById<TextView>(Resource.Id.addressname);
            childTextView = itemView.FindViewById<TextView>(Resource.Id.childname);
            delete = itemView.FindViewById<ImageButton>(Resource.Id.deletebutton);
            delete.Click += (sender, e) => deletepressed(new FamilyadapterClickEventArgs { View = itemView, Position = AdapterPosition });



            itemView.LongClick += (sender, e) => longClickListener(new FamilyadapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class FamilyadapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }

    }
}