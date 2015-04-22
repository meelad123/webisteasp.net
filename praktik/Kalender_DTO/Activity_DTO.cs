using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender_DTO
{
    public class Activity_DTO
    {
        #region constructors
        public Activity_DTO()
        {
        }

        public Activity_DTO(Activity_DTO aDto)
        {
            if (aDto != null)
            {
                this._activity = aDto._activity;
                this._arranger = aDto._arranger;
                this._date = aDto._date;
                this._email = aDto._email;
                this._hemsida = aDto._hemsida;
                this._merinfo = aDto._merinfo;
                this._name = aDto._name;
                this._ort = aDto._ort;
                this._tel = aDto._tel;
            }
        }
        #endregion

        #region variabler
        public DateTime _date;
        public string
            _activity,
            _arranger,
            _ort,
            _name,
            _tel,
            _email,
            _hemsida,
            _merinfo;

        #endregion
    }
}
