using System.Windows;

namespace QuickDeal
{
    public partial class App : Application
    {
        public int CurrentUserID { get; set; }

        public void SetCurrentID (int ID)
        {
            CurrentUserID = ID;
        }
    }
}
