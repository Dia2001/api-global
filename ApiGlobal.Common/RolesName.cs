using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGlobal.Common
{
    public static class RolesName
    {
        public const string ROLE_SYSTEM_ADMIN = "ROLE_SYSTEM_ADMIN";

        #region PRODUCT
        public const string ROLE_EMPLOYEE_EDIT_VIEW_PRODUCT = "ROLE_EMPLOYEE_EDIT_VIEW_PRODUCT";
        #endregion

        #region CART
        public const string ROLE_USER_CAN_VIEW_CART = "ROLE_USER_CAN_VIEW_CART";
        public const string ROLE_USER_CREATE_VIEW_CART = "ROLE_USER_CREATE_VIEW_CART";
        public const string ROLE_USER_DELETE_VIEW_CART = " ROLE_USER_DELETE_VIEW_CART";
        #endregion
    }
}
