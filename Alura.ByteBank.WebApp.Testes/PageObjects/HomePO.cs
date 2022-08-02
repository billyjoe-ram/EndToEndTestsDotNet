using OpenQA.Selenium;

namespace Alura.ByteBank.WebApp.Testes.PageObjects
{
    public class HomePO
    {
        private IWebDriver _driver;
        private By _linkHome;
        private By _linkContaCorrente;
        private By _linkClientes;
        private By _linkAgencia;

        public HomePO(IWebDriver driver)
        {
            _driver = driver;

            _linkHome = By.Id("home");
            _linkContaCorrente = By.Id("contacorrente");
            _linkClientes = By.Id("clientes");
            _linkAgencia = By.Id("agencia");
        }

        public void Navegar(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void LinkHomeClick()
        {
            _driver.FindElement(_linkHome).Click();
        }

        public void LinkContaCorrenteClick()
        {
            _driver.FindElement(_linkContaCorrente).Click();
        }

        public void LinkClientesClick()
        {
            _driver.FindElement(_linkClientes).Click();
        }

        public void LinkAgenciasClick()
        {
            _driver.FindElement(_linkAgencia).Click();
        }
    }
}
