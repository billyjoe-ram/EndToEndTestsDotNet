using Xunit;

using System;
using System.IO;
using System.Linq;
using System.Reflection;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using Alura.ByteBank.WebApp.Testes.PageObjects;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin : IDisposable
    {
        private IWebDriver _driver;

        public AposRealizarLogin()
        {
            _driver = new ChromeDriver(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            );
        }

        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {
            // Arrange
            var loginPo = new LoginPO(_driver);
            loginPo.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPo.PreencherCamposELogar("andre@email.com", "senha01");
            loginPo.ClicarBotaoLogar();

            // Assert
            Assert.Contains("Agência", _driver.PageSource);
        }

        [Fact]
        public void TentaRealizarLoginSemPreencherCampos()
        {
            // Arrange
            var loginPo = new LoginPO(_driver);
            loginPo.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPo.ClicarBotaoLogar();

            // Assert
            Assert.Contains("The Email field is required.", _driver.PageSource);
            Assert.Contains("The Senha field is required.", _driver.PageSource);
        }

        [Fact]
        public void RealizaLoginAcessaMenuECadastraCliente()
        {
            // Arrange
            var loginPo = new LoginPO(_driver);
            var homePo = new HomePO(_driver);

            loginPo.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPo.PreencherCamposELogar("andre@email.com", "senha01");
            loginPo.ClicarBotaoLogar();

            _driver.FindElement(By.LinkText("Cliente")).Click();

            _driver.FindElement(By.LinkText("Adicionar Cliente")).Click();

            _driver.FindElement(By.Name("Identificador")).Click();
            _driver.FindElement(By.Name("Identificador"))
                .SendKeys("2df71922-ca7d-4d43-b142-0767b32f8f22a");

            _driver.FindElement(By.Name("CPF")).Click();
            _driver.FindElement(By.Name("CPF")).SendKeys("");

            _driver.FindElement(By.Name("Nome")).Click();
            _driver.FindElement(By.Name("Nome")).SendKeys("Tobey Garfield");

            _driver.FindElement(By.Name("Profissao")).Click();
            _driver.FindElement(By.Name("Profissao")).SendKeys("Cientista");

            // Act
            _driver.FindElement(By.CssSelector(".btn-primary")).Click();
            homePo.LinkHomeClick();

            // Assert
            Assert.Contains("Logout", _driver.PageSource);
        }

        [Fact]
        public void RealizaLoginAcessaListagemDeContas()
        {
            // Arrange
            var loginPo = new LoginPO(_driver);
            var homePo = new HomePO(_driver);

            loginPo.Navegar("https://localhost:44309/UsuarioApps/Login");

            // Act
            loginPo.PreencherCamposELogar("andre@email.com", "senha01");
            loginPo.ClicarBotaoLogar();
            
            homePo.LinkContaCorrenteClick();
            IReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.TagName("a"));

            var elemento = (from webElemento in elements
                            where webElemento.Text.Contains("Detalhes")
                            select webElemento).First();

            // Act 
            elemento.Click();

            // Assert
            Assert.Contains("Voltar", _driver.PageSource);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
