using Xunit;

using System.IO;
using System.Linq;
using System.Reflection;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using Alura.ByteBank.WebApp.Testes.PageObjects;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin
    {

        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var loginPo = new LoginPO(driver);
            loginPo.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPo.PreencherCamposELogar("andre@email.com", "senha01");
            loginPo.ClicarBotaoLogar();

            // Assert
            Assert.Contains("Agência", driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginSemPreencherCampos()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var loginPo = new LoginPO(driver);
            loginPo.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPo.ClicarBotaoLogar();

            // Assert
            Assert.Contains("The Email field is required.", driver.PageSource);
            Assert.Contains("The Senha field is required.", driver.PageSource);
        }

        [Fact]
        public void RealizaLoginAcessaMenuECadastraCliente()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var loginPo = new LoginPO(driver);
            loginPo.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPo.PreencherCamposELogar("andre@email.com", "senha01");
            loginPo.ClicarBotaoLogar();

            driver.FindElement(By.LinkText("Cliente")).Click();

            driver.FindElement(By.LinkText("Adicionar Cliente")).Click();

            driver.FindElement(By.Name("Identificador")).Click();
            driver.FindElement(By.Name("Identificador"))
                .SendKeys("2df71922-ca7d-4d43-b142-0767b32f8f22a");

            driver.FindElement(By.Name("CPF")).Click();
            driver.FindElement(By.Name("CPF")).SendKeys("");

            driver.FindElement(By.Name("Nome")).Click();
            driver.FindElement(By.Name("Nome")).SendKeys("Tobey Garfield");

            driver.FindElement(By.Name("Profissao")).Click();
            driver.FindElement(By.Name("Profissao")).SendKeys("Cientista");

            // Act
            driver.FindElement(By.CssSelector(".btn-primary")).Click();
            driver.FindElement(By.LinkText("Home")).Click();

            // Assert
            Assert.Contains("Logout", driver.PageSource);
        }

        [Fact]
        public void RealizaLoginAcessaListagemDeContas()
        {
            // Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            var loginPo = new LoginPO(driver);
            loginPo.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPo.PreencherCamposELogar("andre@email.com", "senha01");
            loginPo.ClicarBotaoLogar();
            
            driver.FindElement(By.Id("contacorrente")).Click();
            IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.TagName("a"));

            var elemento = (from webElemento in elements
                            where webElemento.Text.Contains("Detalhes")
                            select webElemento).First();

            // Act 
            elemento.Click();

            // Assert
            Assert.Contains("Voltar", driver.PageSource);
        }
    }
}
