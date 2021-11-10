using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using NRIApp.LocalJobs.Features.Posting.Models;
using System.Linq;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
   public class SkilssVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Skillscommand { get; set; }
        public static List<string> newskilltextvalues { get; set; }
        public static string Checkedvalues;
        public static string Checkedtext;
        IUserDialogs _Dialog = UserDialogs.Instance;
        Postingdata data = new Postingdata();
        public SkilssVM (Postingdata pd)
        {
            try
            {
                data = pd;
                //data.functionalareaid = pd.functionalareaid;
                //data.jobtype = pd.jobtype;
                //data.roleid = pd.roleid;
                //data.rolename = pd.rolename;
                Skillscommand = new Command(Skillsbmt);

            }
            catch (Exception ex)
            {

            }
        }
        public static string newskillvalue = "";
        public void Skillsbmt()
        {
            var currentpage = GetCurrentPage();
            try
            {
               // if(!string.IsNullOrEmpty(Checkedvalues))
               // {
                    string val = Checkedvalues, text = Checkedtext ,checkeddata="";

                    if (val != null && val != "")
                    {
                        if (newskilltextvalues.Count>0)
                        {
                        newskillvalue = String.Join(",", newskilltextvalues);
                        if(!string.IsNullOrEmpty(newskillvalue))
                        {
                            //Checkedtext = Checkedtext + ","+ newskillvalue;
                            checkeddata = Checkedtext + "," + newskillvalue;
                        }
                        }
                        else
                        {
                             checkeddata = Checkedtext;
                        }
                        data.skillids = Checkedvalues;
                        data.skills = checkeddata;
                        currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobdetails(data));
                    }
                    else
                    {
                        if (newskilltextvalues != null && newskilltextvalues.Count!=0)
                        {
                            newskillvalue = String.Join(",", newskilltextvalues);
                            checkeddata = newskillvalue;
                            data.skillids = Checkedvalues;
                            data.skills = checkeddata;
                            currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobdetails(data));
                        }
                        else
                        {
                            _Dialog.Toast("Please choose atleast one category");
                        }

                    }
              //  }
              //  else
               // {
               //     _Dialog.Toast("Please choose atleast one category");
               // }



            }
            catch (Exception ex)
            {
                _Dialog.HideLoading();
            }
        }

        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }

    }
}
