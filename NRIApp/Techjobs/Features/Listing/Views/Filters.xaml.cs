using NRIApp.Techjobs.Features.Listing.Models;
using NRIApp.Techjobs.Features.Listing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Techjobs.Features.Listing.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Filters : ContentPage
	{

        string Checkedvalues = "", trmodes = "", moduleid = "", modulename = "";
        public Filters (string id, string name,string mode, string facilities)
		{
			InitializeComponent ();
            moduleid = id;
            modulename = name;
            Checkedvalues = facilities;
            if (mode == "3")
                mode = "1,2";
            trmodes = mode;
            Binddata(mode,facilities);


        }
        private void Binddata(string mode, string facilities)
        {
            List<facilities> faclty = new List<facilities>();
            faclty.Add(new facilities { facilityname = "Placement", facilityid = "1" });
            faclty.Add(new facilities { facilityname = "Visa Assistance", facilityid = "2" });
            faclty.Add(new facilities { facilityname = "Accommodation", facilityid = "3" });
            faclty.Add(new facilities { facilityname = "Free Training", facilityid = "4" });
            faclty.Add(new facilities { facilityname = "Individual Training", facilityid = "5" });
            faclty.Add(new facilities { facilityname = "Corporate Training", facilityid = "6" });
            faclty.Add(new facilities { facilityname = "Resume Assistance", facilityid = "7" });
            faclty.Add(new facilities { facilityname = "Career Guidance", facilityid = "8" });
            faclty.Add(new facilities { facilityname = "Real Time Projects", facilityid = "9" });
            faclty.Add(new facilities { facilityname = "Mock Interviews", facilityid = "10" });
            faclty.Add(new facilities { facilityname = "IT Consulting", facilityid = "11" });
            faclty.Add(new facilities { facilityname = "Case Study", facilityid = "12" });
            faclty.Add(new facilities { facilityname = "Internship", facilityid = "13" });
            faclty.Add(new facilities { facilityname = "Continuing Education", facilityid = "14" });
            foreach (var item in faclty)
            {

                //Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                BoxView box = new BoxView();
                box.BackgroundColor = Color.FromHex("#e0e0e0e");
                box.HeightRequest = 1;
                //chb.OnImg = "CheckBoxChecked.png";
                //chb.OffImg = "CheckBoxUnChecked.png";
                chb.Text = item.facilityname;
                //chb.ShowLabel = true;
                chb.CheckChanged += Chb_CheckChanged;
                chb.ClassId = item.facilityid.ToString();
                chb.Padding = 10;
                if(facilities!="")
                {
                    string[] facilityids = facilities.Split(',');
                    foreach(var i in facilityids)
                    {
                        if(item.facilityid==i)
                            chb.IsChecked = true;
                    }
                }
                layoutinnercheckbox.Children.Add(chb);
                layoutinnercheckbox.Children.Add(box);
            }

            List<Trainingmodes> modes = new List<Trainingmodes>();
            modes.Add(new Trainingmodes { mode = "Online", modeid = "2" });
            modes.Add(new Trainingmodes { mode = "Inclass", modeid = "1" });
            foreach (var item in modes)
            {

                Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                //chb.OnImg = "CheckBoxChecked.png";
                //chb.OffImg = "CheckBoxUnChecked.png";
                chb.Text = item.mode;
                //chb.ShowLabel = true;
                chb.CheckChanged += Trmodes_CheckChanged;
                chb.ClassId = item.modeid.ToString();
                chb.Padding = 10;
                if (mode != "")
                {
                    string[] traingmodes = mode.Split(',');
                    foreach (var i in traingmodes)
                    {
                        if (item.modeid == i)
                            chb.IsChecked = true;
                    }
                }
                stackmodes.Children.Add(chb);
            }
        }
        private void Chb_CheckChanged(object sender, EventArgs e)
        {
            var b = sender as Plugin.InputKit.Shared.Controls.CheckBox;

            if (b.IsChecked==true)
            {
                if (Checkedvalues != null && Checkedvalues != "")
                {
                    if (!Checkedvalues.Contains(b.ClassId))
                    {
                       Checkedvalues += "," + b.ClassId;
                    }
                }
                else
                {

                   Checkedvalues += b.ClassId;
                }
                //if (Checkedtext != null && Checkedtext != "")
                //{
                //    if (!Checkedtext.Contains(b.Text))
                //    {
                //        Checkedtext += b.Text + ",";
                //    }
                //}
                //else
                //{

                //   Checkedtext += b.Text + ",";
                //}


            }
            else
            {

                string[] indexvalue = Checkedvalues.Split(',');
                int numIndex = Array.IndexOf(indexvalue, b.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
               Checkedvalues = string.Join(",", indexvalue);

                //string[] indextext =Checkedtext.Split(',');
                //int txtIndex = Array.IndexOf(indextext, b.Text);
                //indextext = indextext.Where((val, idx) => idx != txtIndex).ToArray();
                //Checkedtext = string.Join(",", indextext);


            }
        }

        private void Trmodes_CheckChanged(object sender, EventArgs e)
        {
            var b = sender as Plugin.InputKit.Shared.Controls.CheckBox;

            if (b.IsChecked==true)
            {
                if (trmodes != null && trmodes != "")
                {
                    if (!trmodes.Contains(b.ClassId))
                    {
                        trmodes += "," + b.ClassId;
                    }
                }
                else
                {

                    trmodes += b.ClassId;
                }
            }
            else
            {

                string[] indexvalue = trmodes.Split(',');
                int numIndex = Array.IndexOf(indexvalue, b.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                trmodes = string.Join(",", indexvalue);
            }
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (trmodes == "1,2" || trmodes == "2,1")
                trmodes = "3";
            await Navigation.PopModalAsync();
            await Navigation.PushAsync(new NRIApp.Techjobs.Features.Listing.Views.Modulelisting(moduleid,modulename,trmodes,Checkedvalues,"filter",""));
        }

        private async void Clickreset()
        {
            trmodes = ""; Checkedvalues = "";
            await Navigation.PopModalAsync();
            await Navigation.PushAsync(new NRIApp.Techjobs.Features.Listing.Views.Modulelisting(moduleid, modulename, trmodes, Checkedvalues, "filter", ""));
        }

        private async void Backbtn()
        {
            await Navigation.PopModalAsync();
          
        }

    }
}