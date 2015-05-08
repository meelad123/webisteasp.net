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
    [Serializable()]
    public class UsersMod
    {
        #region Constructors
        /******************************************
        Call:       Books()
        Task:       Constructor for Books().
                    Creates a new Books object
        ******************************************/
        //Lazy load
        public UsersMod()
        {
            this.bDTO = new UserDTO();
        }

        //Prevents null values
        public UsersMod(UserDTO bdto)
        {
            bDTO = new UserDTO(bdto);
        }
        #endregion
        


        public string Type 
        { 
            get 
            { 
                return bDTO._type; 
            } 
            set 
            {
                bDTO._type = value; 
            } 
        }
        [Required]
        public string UserName
        {
            get
            {
                return bDTO._username;
            }
            set
            {
                bDTO._username = value;
            }
        }
        [Required]
        public string password
        {
            get
            {
                return bDTO._password;
            }
            set
            {
                bDTO._password = value;
            }
        }

        public string passwordSalt
        {
            get
            {
                return bDTO.passwordSalt;
            }
            set
            {
                bDTO.passwordSalt = value;
            }
        }

        public string userID
        {
            get
            {
                return bDTO.userID;
            }
            set
            {
                bDTO.userID = value;
            }
        }


        public UsersMod checkUser(UsersMod u)
        {
            UserDTO nA = new UserDTO();
            nA._username = u.UserName;
            nA._password = u.password;
            return new UsersMod(DataAccess.checkUser(nA));
        }

        public static void createUser(UsersMod u)
        {
            UserDTO nU = new UserDTO();
            nU.userID = u.userID;
            nU._username = u.UserName;
            nU._password = u.password;
            nU._type = u.Type;
            nU.passwordSalt = u.passwordSalt;
            DataAccess.createUser(nU);
        }
        //userDTO object
        private UserDTO bDTO;
    }
}


