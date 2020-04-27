using BR.Models;
using BR.TenorModel;
using log4net;
using ModelLib.BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BR.TenorLogic
{
   public class HomeService
    {
       private static readonly ILog logger = LogManager.GetLogger(typeof(TenorService));

       public List<HOME> findAll(BRContext db)
       {
           HomeRepository ur = new HomeRepository(db);
           return ur.selectAll();
       }

       public List<HOME> findAll()
       {
           logger.Info("findAll");
           using (BRContext db = new BRContext())
           {
               return findAll(db);
           }
       }
       public List<HomeView> findAllView()
       {
           List<HOME> homes = findAll();
           if (homes == null)
           {
               return new List<HomeView>();
           }
           else
           {
               return convertToModelView(homes);
           }
       }
       public static List<HomeView> convertToModelView(List<HOME> homes)
       {
           List<HomeView> homeViews = new List<HomeView>();
           foreach (HOME home in homes)
           {
               homeViews.Add(convertToModelView(home));
           }
           return homeViews;
       }
       public static HomeView convertToModelView(HOME home)
       {
           HomeView homeView = new HomeView();

           homeView.BankCode = home.BankCode;
           homeView.oneMonth = home.oneMonth;
           homeView.twoMonth = home.twoMonth;
           homeView.threeMonth = home.threeMonth;
           homeView.sixMonth = home.sixMonth;
           homeView.nineMonth = home.nineMonth;
           homeView.twelveMonth = home.twelveMonth;
           homeView.twentyfourMonth = home.twentyfourMonth;
           homeView.thirtysixMonth = home.thirtysixMonth;
           homeView.fortyeightMonth = home.fortyeightMonth;
           homeView.sixtyMonth = home.sixtyMonth;
           return homeView;
       }
        public DataTable DisplayRateHistory(BRContext db, string bankCode)
        {
         
            HomeRepository ur = new HomeRepository(db);
            return ur.display(bankCode);
        }
        public DataTable DisplayRateHistory(string bankCode)
        {

            using (BRContext db = new BRContext())
            {
                return DisplayRateHistory(db, bankCode);
            }
        }
    }
}
