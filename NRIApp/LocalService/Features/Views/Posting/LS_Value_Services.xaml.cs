using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using NRIApp.LocalService.Features.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Posting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_Value_Services : ContentPage
    {
        public static decimal totalamount = 0; decimal chkedamount = 0;
        SELECTED_PACKAGE pack = new SELECTED_PACKAGE();
        IUserDialogs _Dialog = UserDialogs.Instance;
        private List<LS_VALUE_ADD_SERVICE_DATA> _BindValueServices;
        public List<LS_VALUE_ADD_SERVICE_DATA> BindValueServices
        {
            get { return _BindValueServices; }
            set
            {
                _BindValueServices = value;

            }
        }
        public LS_Value_Services(decimal totalamt, string valuepacks, SELECTED_PACKAGE spack)
        {


            InitializeComponent();

            totalamount = totalamt;
            string tamnt = "$" + Convert.ToString(totalamount);
            totalpayamount.Text = tamnt;

            pack.amount = spack.amount;
            pack.mcamount = spack.mcamount;
            pack.mcoriginalamt = spack.mcoriginalamt;
            pack.mcsavepercentage = spack.mcsavepercentage;
            pack.mcsaveperday = spack.mcsaveperday;
            pack.noofdays = spack.noofdays;
            pack.addtionalcitiesamount = spack.addtionalcitiesamount;
            pack.packageamount = spack.packageamount;
            pack.valueserviceamount = spack.valueserviceamount;
            // BindvalueaddServices(valuepacks);
            // LS_ViewModel.Valueaddedcontentid = "";


        }

        public async void BindvalueaddServices(string valuepacks)
        {
            try
            {
                _Dialog.ShowLoading("");
                var bindservices = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                var result = await bindservices.GetValueaddedservices(pack.noofdays);
                BindValueServices = result.ROW_DATA;
                var curpage = LS_ViewModel.GetCurrentPage();
                var mainstack = curpage.FindByName<StackLayout>("stackvalueaddedservices");
                mainstack.Children.Clear();
                foreach (var item in result.ROW_DATA)
                {



                    Frame frm = new Frame()
                    {
                        CornerRadius = 4,
                        BackgroundColor = Color.White,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = 0
                    };

                    StackLayout stack1 = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = 15
                    };
                    StackLayout stack2 = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    };

                    StackLayout chkstack3 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = new Thickness(0, 5, 0, 5)
                    };

                    AsNum.XFControls.CheckBox chb = new AsNum.XFControls.CheckBox();
                    chb.Text = item.title;
                    chb.OnImg = "CheckBoxChecked.png";
                    chb.OffImg = "CheckBoxUnChecked.png";
                    chb.ShowLabel = true;
                    if (LS_ViewModel.Valueaddedcontentid.Contains(item.addonamountbasic_contentid))
                    {
                        chb.Checked = true;
                        LS_ViewModel.valueadded = LS_ViewModel.valueadded + 1;
                        chkedamount = chkedamount + Convert.ToDecimal(item.addonamountbasic);
                    }
                    chb.ClassId = Convert.ToString(item.addonamountbasic) + "," + item.addonamountbasic_contentid;
                    chb.CheckChanged += Valueaddchk_CheckChanged;

                    StackLayout valueschkstack4 = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(30, 0, 0, 0),

                    };
                    Label lblamt = new Label()
                    {
                        FontSize = 16,
                        FontFamily = "Montserrat-SemiBold.ttf#Montserrat-SemiBold",
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#08a182"),
                        Text = Convert.ToString(item.addonamountbasic)
                    };
                    Grid gd = new Grid()
                    {
                        HorizontalOptions = LayoutOptions.Start
                    };
                    Label lblorgamt = new Label()
                    {
                        FontSize = 14,
                        FontFamily = "Montserrat-SemiBold.ttf#Montserrat-SemiBold",
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#888888"),
                        Text = Convert.ToString(item.addonamountbasic_originalamt)
                    };
                    BoxView orgamtbx = new BoxView()
                    {
                        HeightRequest = 1,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Opacity = 0.8,
                        Color = Color.FromHex("#888888"),
                        WidthRequest = 20
                    };
                    Label lblperdayamt = new Label()
                    {
                        FontSize = 11,
                        FontFamily = "Montserrat-SemiBold.ttf#Montserrat-SemiBold",
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#888888"),
                        Margin = new Thickness(15, 0, 0, 0),
                        Text = item.addonamountbasic_saveperday
                    };
                    Label lblperdaysaveper = new Label()
                    {
                        FontSize = 11,
                        FontFamily = "Montserrat-SemiBold.ttf#Montserrat-SemiBold",
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromHex("#888888"),

                        Text = item.addonamountbasic_savepercentage
                    };

                    BoxView bx2 = new BoxView()
                    {
                        BackgroundColor = Color.FromHex("#e7eaec"),
                        HeightRequest = 1,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(0)
                    };

                    StackLayout stackloop = new StackLayout()
                    {
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                    };

                    string[] features = item.features.Split(',');
                    string fdata = "", finaldata = "";
                    for (int i = 0; i < features.Length; i++)
                    {

                        StackLayout stackloopdata = new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };
                        Image img = new Image()
                        {
                            Source = "TickIcon.png",
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.Start,
                            HeightRequest = 14,
                            WidthRequest = 14,
                            Margin = new Thickness(0)
                        };
                        Label featurelbl = new Label()
                        {
                            FontFamily = "Montserrat-Medium.ttf#Montserrat-Medium",

                            //                         FontFamily = Device.RuntimePlatform == Device.iOS ? "Roboto-Bold.ttf#FontRegular" :
                            //Device.RuntimePlatform == Device.Android ? "Roboto-Bold.ttf#FontRegular" : "Assets/Lobster-Regular.ttf#Lobster",
                            TextColor = Color.FromHex("#474747"),
                            FontSize = 12,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.Start,
                            Text = features[i]
                        };

                        stackloopdata.Children.Add(img);
                        stackloopdata.Children.Add(featurelbl);

                        stackloop.Children.Add(stackloopdata);

                    }

                    gd.Children.Add(lblorgamt);
                    gd.Children.Add(orgamtbx);
                    valueschkstack4.Children.Add(lblamt);
                    valueschkstack4.Children.Add(gd);
                    valueschkstack4.Children.Add(lblperdayamt);
                    valueschkstack4.Children.Add(lblperdaysaveper);

                    chkstack3.Children.Add(chb);

                    stack2.Children.Add(chkstack3);
                    stack2.Children.Add(valueschkstack4);
                    stack2.Children.Add(bx2);
                    stack1.Children.Add(stack2);
                    stack1.Children.Add(stackloop);

                    frm.Content = stack1;

                    mainstack.Children.Add(frm);

                }
                _Dialog.HideLoading();


                var btnnext = curpage.FindByName<Button>("btnvaluenext");
                var btnskip = curpage.FindByName<Button>("btnvalueskip");
                if (LS_ViewModel.valueadded != 0)
                {
                    btnnext.IsVisible = true;
                    btnskip.IsVisible = false;
                }
                else
                {
                    btnnext.IsVisible = false;
                    btnskip.IsVisible = true;
                }

            }
            catch (Exception ee)
            {

            }




        }


        private void Valueaddchk_CheckChanged(object sender, EventArgs e)
        {
            try
            {
                var chk = sender as AsNum.XFControls.CheckBox;
                var curpage = LS_ViewModel.GetCurrentPage();
                var btnnext = curpage.FindByName<Button>("btnvaluenext");
                var btnskip = curpage.FindByName<Button>("btnvalueskip");
                if (chk.Checked)
                {

                    string[] checkedvalues = chk.ClassId.Split(',');
                    LS_ViewModel.valueadded = LS_ViewModel.valueadded + 1;
                    chkedamount = chkedamount + Convert.ToDecimal(checkedvalues[0]);
                    totalamount = totalamount + Convert.ToDecimal(checkedvalues[0]);
                    totalpayamount.Text = "$" + Convert.ToString(totalamount);
                    LS_ViewModel.Valueaddedcontentid += checkedvalues[1] + ",";
                    pack.amount = pack.amount + Convert.ToDecimal(checkedvalues[0]);

                }
                else
                {
                    string[] checkedvalues = chk.ClassId.Split(',');
                    LS_ViewModel.valueadded = LS_ViewModel.valueadded - 1;
                    chkedamount = chkedamount - Convert.ToDecimal(checkedvalues[0]);
                    totalamount = totalamount - Convert.ToDecimal(checkedvalues[0]);
                    totalpayamount.Text = "$" + Convert.ToString(totalamount);

                    string[] indexvalue = LS_ViewModel.Valueaddedcontentid.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, checkedvalues[1].ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    LS_ViewModel.Valueaddedcontentid = string.Join(",", indexvalue);
                    pack.amount = pack.amount - Convert.ToDecimal(checkedvalues[0]);
                }
                pack.valueserviceamount = chkedamount;
                if (LS_ViewModel.valueadded != 0)
                {
                    btnnext.IsVisible = true;
                    btnskip.IsVisible = false;
                }
                else
                {
                    btnnext.IsVisible = false;
                    btnskip.IsVisible = true;
                }
            }
            catch (Exception ee)
            {

            }
        }

        private async void Backbtncommand(object sender, EventArgs e)
        {
            //LS_ViewModel.Valueaddedcontentid = "";
            //LS_ViewModel.valueadded = 0;

            //if (LS_ViewModel.valueadded == 1)
            //{
            //    pack.amount = pack.amount - pack.valueserviceamount;
            //}
            //if (LS_ViewModel.valueadded == 0)
            //{
            //    pack.valueserviceamount = 0;
            //}
            var curpage = LS_ViewModel.GetCurrentPage();
            curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
            await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Additional_Cities(LS_ViewModel.addionalcities.Count, pack));
            curpage.Navigation.RemovePage(this);
        }
        private async void Btnvalueserviceskip(object sender, EventArgs e)
        {
            var curpage = LS_ViewModel.GetCurrentPage();
            // LS_ViewModel.Valueaddedcontentid = "";
            //  LS_ViewModel.valueadded = 0;
            if (chkedamount != 0)
            {
                totalamount = totalamount - chkedamount;
                LS_ViewModel.Valueaddedcontentid = "";
                LS_ViewModel.valueadded = 0;
                pack.valueserviceamount = 0;
                chkedamount = 0;
                if (BindValueServices != null)
                {
                    var mainstack = curpage.FindByName<StackLayout>("stackvalueaddedservices");

                    mainstack.Children.Clear();
                    foreach (var item in BindValueServices)
                    {



                        Frame frm = new Frame()
                        {
                            CornerRadius = 4,
                            BackgroundColor = Color.White,
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Padding = 0
                        };

                        StackLayout stack1 = new StackLayout()
                        {
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Padding = 15
                        };
                        StackLayout stack2 = new StackLayout()
                        {
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                        };

                        StackLayout chkstack3 = new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Padding = new Thickness(0, 5, 0, 5)
                        };

                        AsNum.XFControls.CheckBox chb = new AsNum.XFControls.CheckBox();
                        chb.Text = item.title;
                        chb.OnImg = "CheckBoxChecked.png";
                        chb.OffImg = "CheckBoxUnChecked.png";
                        chb.ShowLabel = true;
                        if (LS_ViewModel.Valueaddedcontentid.Contains(item.addonamountbasic_contentid))
                        {
                            chb.Checked = true;
                            LS_ViewModel.valueadded = LS_ViewModel.valueadded + 1;
                            chkedamount = chkedamount + Convert.ToDecimal(item.addonamountbasic);
                        }
                        chb.ClassId = Convert.ToString(item.addonamountbasic) + "," + item.addonamountbasic_contentid;
                        chb.CheckChanged += Valueaddchk_CheckChanged;

                        StackLayout valueschkstack4 = new StackLayout()
                        {
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Margin = new Thickness(30, 0, 0, 0),

                        };
                        Label lblamt = new Label()
                        {
                            FontSize = 16,
                            FontFamily = "Montserrat-SemiBold.ttf#Montserrat-SemiBold",
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = Color.FromHex("#08a182"),
                            Text = Convert.ToString(item.addonamountbasic)
                        };
                        Grid gd = new Grid()
                        {
                            HorizontalOptions = LayoutOptions.Start
                        };
                        Label lblorgamt = new Label()
                        {
                            FontSize = 14,
                            FontFamily = "Montserrat-SemiBold.ttf#Montserrat-SemiBold",
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = Color.FromHex("#888888"),
                            Text = Convert.ToString(item.addonamountbasic_originalamt)
                        };
                        BoxView orgamtbx = new BoxView()
                        {
                            HeightRequest = 1,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Center,
                            Opacity = 0.8,
                            Color = Color.FromHex("#888888"),
                            WidthRequest = 20
                        };
                        Label lblperdayamt = new Label()
                        {
                            FontSize = 11,
                            FontFamily = "Montserrat-SemiBold.ttf#Montserrat-SemiBold",
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = Color.FromHex("#888888"),
                            Margin = new Thickness(15, 0, 0, 0),
                            Text = item.addonamountbasic_saveperday
                        };
                        Label lblperdaysaveper = new Label()
                        {
                            FontSize = 11,
                            FontFamily = "Montserrat-SemiBold.ttf#Montserrat-SemiBold",
                            HorizontalOptions = LayoutOptions.Start,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = Color.FromHex("#888888"),

                            Text = item.addonamountbasic_savepercentage
                        };

                        BoxView bx2 = new BoxView()
                        {
                            BackgroundColor = Color.FromHex("#e7eaec"),
                            HeightRequest = 1,
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Margin = new Thickness(0)
                        };

                        StackLayout stackloop = new StackLayout()
                        {
                            VerticalOptions = LayoutOptions.Start,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };

                        string[] features = item.features.Split(',');
                        string fdata = "", finaldata = "";
                        for (int i = 0; i < features.Length; i++)
                        {

                            StackLayout stackloopdata = new StackLayout()
                            {
                                Orientation = StackOrientation.Horizontal,
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalOptions = LayoutOptions.FillAndExpand
                            };
                            Image img = new Image()
                            {
                                Source = "TickIcon.png",
                                VerticalOptions = LayoutOptions.Start,
                                HorizontalOptions = LayoutOptions.Start,
                                HeightRequest = 14,
                                WidthRequest = 14,
                                Margin = new Thickness(0)
                            };
                            Label featurelbl = new Label()
                            {
                                FontFamily = "Montserrat-Medium.ttf#Montserrat-Medium",

                                //                         FontFamily = Device.RuntimePlatform == Device.iOS ? "Roboto-Bold.ttf#FontRegular" :
                                //Device.RuntimePlatform == Device.Android ? "Roboto-Bold.ttf#FontRegular" : "Assets/Lobster-Regular.ttf#Lobster",
                                TextColor = Color.FromHex("#474747"),
                                FontSize = 12,
                                VerticalOptions = LayoutOptions.CenterAndExpand,
                                HorizontalOptions = LayoutOptions.Start,
                                Text = features[i]
                            };

                            stackloopdata.Children.Add(img);
                            stackloopdata.Children.Add(featurelbl);

                            stackloop.Children.Add(stackloopdata);

                        }

                        gd.Children.Add(lblorgamt);
                        gd.Children.Add(orgamtbx);
                        valueschkstack4.Children.Add(lblamt);
                        valueschkstack4.Children.Add(gd);
                        valueschkstack4.Children.Add(lblperdayamt);
                        valueschkstack4.Children.Add(lblperdaysaveper);

                        chkstack3.Children.Add(chb);

                        stack2.Children.Add(chkstack3);
                        stack2.Children.Add(valueschkstack4);
                        stack2.Children.Add(bx2);
                        stack1.Children.Add(stack2);
                        stack1.Children.Add(stackloop);

                        frm.Content = stack1;

                        mainstack.Children.Add(frm);

                    }
                }
            }
            pack.amount = totalamount;
            totalpayamount.Text = "$" + Convert.ToString(pack.amount);
            await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Order_Summary(pack));
        }
        private async void Valueaddedbtn_Clicked(object sender, EventArgs e)
        {
            if (LS_ViewModel.valueadded != 0)
            {
                var curpage = LS_ViewModel.GetCurrentPage();
                pack.amount = totalamount;
                await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Order_Summary(pack));
            }
            else
            {
                _Dialog.Toast("Please select any service");
            }


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindvalueaddServices("");
            chkedamount = 0;
            pack.amount = totalamount;
            if (LS_ViewModel.addionalcities.Count == 0)
            {
                pack.addtionalcitiesamount = 0;
            }
            totalpayamount.Text = "$" + Convert.ToString(totalamount);
        }
    }
}