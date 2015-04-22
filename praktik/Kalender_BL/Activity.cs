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
        [Display(Name = "Datum")]
        public DateTime Date 
        { 
            get { return _aDTO._date; }
            set { _aDTO._date = value; }
        }

        [Display(Name = "Aktivitet")]
        public string Activitet
        {
            get { return _aDTO._activity; }
            set { _aDTO._activity = value; }
        }

        [Display(Name = "Arrangör")]
        public string Arrangor
        {
            get { return _aDTO._arranger; }
            set { _aDTO._arranger = value; }
        }

        [Display(Name = "Ort")]
        public string Ort
        {
            get { return _aDTO._ort; }
            set { _aDTO._ort = value; }
        }

        [Display(Name = "Namn")]
        public string Namn
        {
            get { return _aDTO._name; }
            set { _aDTO._name = value; }
        }

        [Display(Name = "Mobil")]
        public string Tel
        {
            get { return _aDTO._tel; }
            set { _aDTO._tel = value; }
        }
        [Display(Name = "E-post")]
        public string Email
        {
            get { return _aDTO._email; }
            set { _aDTO._email = value; }
        }

        public string Hemsida
        {
            get { return _aDTO._hemsida; }
            set { _aDTO._hemsida = value; }
        }
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
        #endregion
    }
}
