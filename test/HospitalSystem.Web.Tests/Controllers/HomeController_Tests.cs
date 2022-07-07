using System.Threading.Tasks;
using HospitalSystem.Models.TokenAuth;
using HospitalSystem.Web.Controllers;
using Shouldly;
using Xunit;

namespace HospitalSystem.Web.Tests.Controllers
{
    public class HomeController_Tests: HospitalSystemWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}