using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product Added!";
        public static string ProductUpdated = "Product Updated!";
        public static string ProductDeleted = "Product Deleted!";
        public static string ProductsListed = "Products Listed!";
        public static string ProductListed = "Product Listed!";
        public static string ProductDetailListed = "The Detail of Products Listed!";
        public static string ProductsNotListed = "Products Not Listed!";
        public static string ProductNameErrorMessage = "Product's Name is Incorrect!";
        public static string AuthorizationDenied = "You are not authorized";
        public static string UserRegistered = "User Registered!";
        public static string UserNotFound = "User not found!";
        public static string PasswordError = "Password is Incorrect!";
        public static string SuccessfulLogin = "Login Successful!";
        public static string AccessTokenCreated = "Access Token Created!";
        public static string UserAlreadyExists = "User Already Exists";
    }
}
