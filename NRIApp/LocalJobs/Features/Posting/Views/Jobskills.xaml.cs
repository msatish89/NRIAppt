
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalJobs.Features.Posting.Models;
using NRIApp.LocalJobs.Features.Posting.ViewModels;
using NRIApp.LocalService.Features.Views;
using Xamarin.Forms.Extended;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Jobskills : ContentPage
	{
        IUserDialogs _Dialog = UserDialogs.Instance;
        public InfiniteScrollCollection<Skillslist> Skills { get; set; }
        List<string> newskillList = new List<string>();
        Postingdata jdata = new Postingdata();
        public Jobskills (Postingdata pd)
		{
			InitializeComponent ();
            Title = "Select the Skills";
            jdata = pd;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");

            //  addnewskillvisible.IsVisible = false;
            skillsloader.IsVisible = false;
            if(pd.ismyneed=="1")
            {
                SkilssVM.Checkedtext = pd.Tags;
            }
            BindingLeaftypes(pd.functionalareaid);
            this.BindingContext = new SkilssVM(pd);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SkilssVM.newskillvalue = "";
            SkilssVM.newskilltextvalues = new List<string>();
            //checkedemployementtypevalue = "";
        }

        private List<Skillslist> _getlsleafdata { get; set; }
        public List<Skillslist> getlsleafdata
        {
            get { return _getlsleafdata; }

            set
            {
                _getlsleafdata = value;
                OnPropertyChanged(nameof(getlsleafdata));
            }
        }
        public int skillpageno = 1;
        public int SkillID;
        public async void BindingLeaftypes(int id)
        {
            try
            {
                _Dialog.ShowLoading("", null);
                skillpageno = 1;
                SkillID = id;
                //SkillID = 84;
                var listingdata = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                var data = await listingdata.GetSkills(id,1);
                _getlsleafdata = data.ROW_DATA;
                pageno = data.ROW_DATA.Last().pageno;
                if (getlsleafdata != null && getlsleafdata.Count > 0)
                {

                    foreach (var item in data.ROW_DATA)
                    {
                        BoxView box = new BoxView();
                        box.BackgroundColor = Color.FromHex("e0e0e0");
                        box.HeightRequest = 1;
                        Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                        //chb.OnImg = "CheckBoxChecked.png";
                        //chb.OffImg = "CheckBoxUnChecked.png";
                        chb.Text = item.primarytag;
                        //chb.ShowLabel = true;
                        chb.Padding = 10;
                        chb.CheckChanged += Chb_CheckChanged;
                        chb.ClassId = item.primarytagid.ToString();
                        if (jdata.ismyneed == "1")
                        {
                            if (!string.IsNullOrEmpty(jdata.Tags))
                            {
                                string[] skillst = jdata.Tags.Split(',');
                                foreach (var i in skillst)
                                {
                                    if(i==item.primarytag)
                                    {
                                        chb.IsChecked = true;
                                        if(string.IsNullOrEmpty(SkilssVM.Checkedvalues))
                                        {
                                            SkilssVM.Checkedvalues += chb.ClassId;
                                        }
                                        else
                                        {
                                            SkilssVM.Checkedvalues += "," + chb.ClassId;
                                        }
                                    }
                                }
                            }
                        }
                        layoutinnercheckbox.Children.Add(chb);
                        layoutinnercheckbox.Children.Add(box);
                        
                    }

                    if(data.ROW_DATA.Last().totalrecs>25)
                    {
                        Skillsmoreblk.IsVisible = true;
                    }
                }
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }
        public async void Morejobskills(object sender, EventArgs e)
        {
            try
            {
                skillsloader.IsVisible = true;
                skillsloader.IsRunning = true;
                skillpageno = skillpageno + 1;
                var listingdata = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                var data = await listingdata.GetSkills(SkillID, skillpageno);
                _getlsleafdata = null;
                _getlsleafdata = data.ROW_DATA;
                pageno = data.ROW_DATA.Last().pageno;
                if (getlsleafdata != null && getlsleafdata.Count > 0)
                {

                    foreach (var item in data.ROW_DATA)
                    {
                        BoxView box = new BoxView();
                        box.BackgroundColor = Color.FromHex("e0e0e0");
                        box.HeightRequest = 1;
                        Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                        //chb.OnImg = "CheckBoxChecked.png";
                        //chb.OffImg = "CheckBoxUnChecked.png";
                        chb.Text = item.primarytag;
                        //chb.ShowLabel = true;
                        chb.Padding = 10;
                        chb.CheckChanged += Chb_CheckChanged;
                        chb.ClassId = item.primarytagid.ToString();
                        layoutinnercheckbox.Children.Add(chb);
                        layoutinnercheckbox.Children.Add(box);
                    }

                    if (data.ROW_DATA.Last().totalrecs > 25)
                    {
                        Skillsmoreblk.IsVisible = true;
                    }
                }

                skillsloader.IsRunning = false;
                skillsloader.IsVisible = false;
            }
            catch(Exception ex)
            {

            }
        }
        private void Chb_CheckChanged(object sender, EventArgs e)
        {
            var b = sender as Plugin.InputKit.Shared.Controls.CheckBox;
            if (b.IsChecked==true)
            {
                if (SkilssVM.Checkedvalues != null && SkilssVM.Checkedvalues != "")
                {
                    if (!SkilssVM.Checkedvalues.Contains(b.ClassId))
                    {
                        //SkilssVM.Checkedvalues += b.ClassId + ",";
                        SkilssVM.Checkedvalues += "," + b.ClassId ;
                    }
                }
                else
                {

                    //SkilssVM.Checkedvalues += b.ClassId + ",";
                    SkilssVM.Checkedvalues += b.ClassId;
                }
                if (SkilssVM.Checkedtext != null && SkilssVM.Checkedtext != "")
                {
                    if (!SkilssVM.Checkedtext.Contains(b.Text))
                    {
                        //SkilssVM.Checkedtext += b.Text + ",";
                        SkilssVM.Checkedtext += ","+ b.Text;
                    }
                }
                else
                {

                    //SkilssVM.Checkedtext += b.Text + ",";
                    SkilssVM.Checkedtext += b.Text;
                }


            }
            else
            {
                string[] indexvalue = SkilssVM.Checkedvalues.Split(',');
                int numIndex = Array.IndexOf(indexvalue, b.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                SkilssVM.Checkedvalues = string.Join(",", indexvalue);

                string[] indextext = SkilssVM.Checkedtext.Split(',');
                int txtIndex = Array.IndexOf(indextext, b.Text);
                indextext = indextext.Where((val, idx) => idx != txtIndex).ToArray();
                SkilssVM.Checkedtext = string.Join(",", indextext);
            }
        }
        int pageno = 0;
        private async void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            try
            {
                var page = pageno + 1;
                //actindi.IsVisible = true;
                //actindi.IsRunning = true;
                var items = new List<Skillslist>();
                var listingdata = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                var list = await listingdata.GetSkills(84, page);
                items = list.ROW_DATA;
                pageno = list.ROW_DATA.Last().pageno;
                //actindi.IsVisible = false;
                //actindi.IsRunning = false;
                // IsBusy = false;
                if (items != null && items.Count > 0)
                {
                    foreach (var item in list.ROW_DATA)
                    {
                        BoxView box = new BoxView();
                        box.BackgroundColor = Color.FromHex("e0e0e0");
                        box.HeightRequest = 1;
                        Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                        //chb.OnImg = "CheckBoxChecked.png";
                        //chb.OffImg = "CheckBoxUnChecked.png";
                        chb.Text = item.primarytag;
                        //chb.ShowLabel = true;
                        chb.Padding = 10;
                        chb.CheckChanged += Chb_CheckChanged;
                        chb.ClassId = item.primarytagid.ToString();
                        layoutinnercheckbox.Children.Add(chb);
                        layoutinnercheckbox.Children.Add(box);
                    }
                }
            }
            catch (Exception ex) { }
        }

   
        public bool newskillvalidation()
        {
            bool result = true;
            
            if (string.IsNullOrEmpty(addnewskillstxt.Text) || addnewskillstxt.Text.Trim().Length==0)
            {
                result = false;
                _Dialog.Toast("Please enter your skill");
            }
            return result;
        }
    
        private async void addnewskillscmd(object sender, EventArgs e)
        {
            try
            {
                 _Dialog.ShowLoading("", null);
                string result = "true";
                if(newskillvalidation())
                {
                    
                    var addskillAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                    var addskilldata = await addskillAPI.addskill(addnewskillstxt.Text);
                    if(addskilldata.ROW_DATA.Last().resultinfo == 1)
                    {
                        _Dialog.Toast("You are trying to add existing category.");
                    }
                    else
                    {
                        if(_getlsleafdata!=null)
                        {
                            foreach (var item in _getlsleafdata)
                            {
                                if (item.primarytag == addnewskillstxt.Text)
                                {
                                    _Dialog.Toast("You are trying to add existing category.");
                                    result = "false";
                                }
                            }
                            if(result=="true")
                            {
                                string skil = addnewskillstxt.Text;
                                newskillList.Add(skil);

                                addnewskillstxt.Text = "";
                                addnewskillList.ItemsSource = null;
                                addnewskillList.ItemsSource = newskillList;
                                SkilssVM.newskilltextvalues = newskillList;
                                //newskilltextvalues = null;
                                //newskilltextvalues = newskillList;
                                //foreach (var data in newskillList)
                                //{
                                //    data.newskilltxt = data.newskilltxt + ",";
                                //}
                                //newskilltextvalues = newskillList;
                            }
                        }
                     
                       
                    }
                    //else
                    //{
                    //    _Dialog.Toast("invalid category...");
                    //}
                }
                
                _Dialog.HideLoading();
            }
            catch(Exception ex)
            {

            }
           // addnewskillstxt
        }


        private void AddnewskillList_SelectedItemChanged(object sender, EventArgs e)
        {
           try
            {
                string tapskilltext = "";
                var radiogrp = sender as HVScrollGridView;
                var fgh = newskillList;
              //  string lsow = new string();
             string  lsow = (string)radiogrp.SelectedItem;

                foreach (var item in newskillList)
                {
                    if (item == lsow)
                    {
                       tapskilltext = item;
                      // newskillList.Remove(item);
                    }
                }
                newskillList.Remove(tapskilltext);
                addnewskillList.ItemsSource = null;
                addnewskillList.ItemsSource = newskillList;
                SkilssVM.newskilltextvalues = null;
                SkilssVM.newskilltextvalues = newskillList;
            }
            catch(Exception ex)
            {

            }
        }

        //public void btn_leafsubmit(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        string val = Checkedvalues, text = Checkedtext;
        //        if (val != null && val != "")
        //        {
        //            _Dialog.ShowLoading("", null);

        //            _Dialog.HideLoading();
        //        }
        //        else
        //        {
        //            _Dialog.Toast("Please choose atleast one category");
        //        }

        //        Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobdetails(pd));

        //    }
        //    catch (Exception ex) { _Dialog.HideLoading(); }
        //}
    }
}