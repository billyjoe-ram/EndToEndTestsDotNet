using Xunit;

using System.IO;
using System.Reflection;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AluraByteBankWebAppTestes
{
    public class NavegandoNaPaginaHome
    {
        [Fact]
        public void CarregaPaginaHomeEVerificaTituloDaPagina()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // Act
            driver.Navigate().GoToUrl("https://localhost:44309");

            // Assert
            Assert.Contains("WebApp", driver.Title);
        }

        [Fact]
        public void CarregaPaginaHomeVerificaExistenciaLinkLoginEHome()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // Act
            driver.Navigate().GoToUrl("https://localhost:44309");

            // Assert
            Assert.Contains("Login", driver.PageSource);
            Assert.Contains("Home", driver.PageSource);
        }
    }
}