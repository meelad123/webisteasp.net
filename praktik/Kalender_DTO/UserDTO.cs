using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalender_DTO
{
    [Serializable()]
    public class UserDTO
    {
        #region constuctors

        /***************************************
        Task:       Different constructors for
                    User data transfer object
        ****************************************/

        //Lazy loading
        public UserDTO()
        {
        }

        //Preventing null values 
        public UserDTO(UserDTO bDto)
        {
            if (bDto != null)
            {
                this._username = bDto._username;
                this._password = bDto._password;
                this._type = bDto._type;
                this.userID = bDto.userID;
                this.passwordSalt = bDto.passwordSalt;
            }
        }
        #endregion
        public string _username, _password, _type, passwordSalt, userID;
    }
    
}
