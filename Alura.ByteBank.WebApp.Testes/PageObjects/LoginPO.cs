using OpenQA.Selenium;

namespace Alura.ByteBank.WebApp.Testes.PageObjects
{
    public class LoginPO
    {
        private IWebDriver _driver;
        private By _campoEmail;
        private By _campoSenha;
        private By _btnLogar;

        public LoginPO(IWebDriver driver)
        {
            _driver = driver;
            _campoEmail = By.Id("Email");
            _campoSenha = By.Id("Senha");
            _btnLogar = By.Id("btn-logar");
        }

        public void Navegar(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void PreencherCamposELogar(string email, string senha)
        {
            _driver.FindElement(_campoEmail).SendKeys(email);
            _driver.FindElement(_campoSenha).SendKeys(senha);
        }

        public void ClicarBotaoLogar()
        {
            _driver.FindElement(_btnLogar).Click();
        }
    }
}
