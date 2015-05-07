using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalender_DAL;
using Kalender_DTO;
using System.ComponentModel.DataAnnotations;

namespace Kalender_BL
{
    public class Activity
    {
        #region constructors

        public Activity()
        {
            this._aDTO = new Activity_DTO();
        }

        public Activity(Activity_DTO adto)
        {
            _aDTO = new Activity_DTO(adto);
        }
        #endregion

        #region variabler
        private Activity_DTO _aDTO;
        #endregion

        #region properties
        [Required(ErrorMessage = "Kan inte vara tom!")]
        [Display(Name = "Datum")]
        [DataType(DataType.DateTime, ErrorMessage="Fel datum format!")]
        public DateTime Datum
        { 
            get { return _aDTO._date; }
            set { _aDTO._date = value; }
        }
        [Required(ErrorMessage = "Kan inte vara tom!")]
        [Display(Name = "Aktivitet")]
        public string Activitet
        {
            get { return _aDTO._activity; }
            set { _aDTO._activity = value; }
        }
        [Required(ErrorMessage = "Kan inte vara tom!")]
        [Display(Name = "Arrangör")]
        public string Arrangor
        {
            get { return _aDTO._arranger; }
            set { _aDTO._arranger = value; }
        }
        [Required(ErrorMessage = "Kan inte vara tom!")]
        [Display(Name = "Ort")]
        public string Ort
        {
            get { return _aDTO._ort; }
            set { _aDTO._ort = value; }
        }
        [Required(ErrorMessage = "Kan inte vara tom!")]
        [Display(Name = "Namn")]
        public string Namn
        {
            get { return _aDTO._name; }
            set { _aDTO._name = value; }
        }
        [Required(ErrorMessage = "Kan inte vara tom!")]
        [Display(Name = "Mobil")]
        public string Tel
        {
            get { return _aDTO._tel; }
            set { _aDTO._tel = value; }
        }
        [Required(ErrorMessage = "Kan inte vara tom!")]
        [EmailAddress(ErrorMessage = "Ogiltig e-post!")]
        [Display(Name = "E-post")]
        public string Email
        {
            get { return _aDTO._email; }
            set { _aDTO._email = value; }
        }
        [Url(ErrorMessage = "Ogiltig url. Den ska vara som: http://www.example.se")]
        [Required(ErrorMessage = "Kan inte vara tom!")]
        public string Hemsida
        {
            get { return _aDTO._hemsida; }
            set { _aDTO._hemsida = value; }
        }
        [Required(ErrorMessage = "Kan inte vara tom!")]
        [Display(Name = "Mer info")]
        public string MerInfo
        {
            get { return _aDTO._merinfo; }
            set { _aDTO._merinfo = value; }
        }
        #endregion

        #region functions

        public static List<Activity> getAllActivities()
        {
            List<Activity_DTO> aL = DataAccess.getAllActivities();
            List<Activity> nA = new List<Activity>();
            foreach (Activity_DTO a in aL)
            {
                nA.Add(new Activity(a));
            }
            return nA;
        }

        public static List<string> getActivityName()
        {
            List<string> aL = DataAccess.getActivityName();
            return aL;
        }

        public static void insertActivity(Activity a)
        {
            Activity_DTO aDTO = new Activity_DTO();
            aDTO._activity = a.Activitet;
            aDTO._arranger = a.Arrangor;
            aDTO._date = a.Datum;
            aDTO._email = a.Email;
            aDTO._hemsida = a.Hemsida;
            aDTO._merinfo = a.MerInfo;
            aDTO._name = a.Namn;
            aDTO._ort = a.Ort;
            aDTO._tel = a.Tel;
            DataAccess.insertActivity(aDTO);
        }
        public static Activity getActivityByName(string aNamn, DateTime aDatum)
        {
            Activity b = new Activity(DataAccess.getActivityByName(aNamn, aDatum));
            return b;
        }

        public void saveActivity()
        {
            DataAccess.saveActivity(this._aDTO);
        }

        public void removeActivity()
        {
            DataAccess.removeActivity(this._aDTO);
        }
        #endregion
    }
}
