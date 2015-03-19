using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Domain.Models;
using ContactBook.WebApi.Controllers;
using Xunit;

namespace ContactBook.WebApi.Test
{
   public class ApiAccountControllerTest
    {
       [Fact]
       public void GetUserInfoTest()
       {
           var controller = new ApiAccountController();
           UserInfoViewModel model = controller.GetUserInfo();
           Assert.True(model != null, "UserInfoViewModel shouldn't be null");
       }
       
    }
}
