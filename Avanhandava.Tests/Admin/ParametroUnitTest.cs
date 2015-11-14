using Avanhandava.Domain.Abstract;
using Avanhandava.Domain.Models.Admin;
using Avanhandava.Domain.Service.Admin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Avanhandava.Tests
{
    [TestClass]
    public class ParametroUnitTest
    {
        IBaseService<SistemaParametro> service;

        public ParametroUnitTest()
        {
            service = new SistemaParametroService();
        }

        [TestMethod]
        public void IncluirParametro()
        {
            // Arrange
            var parametro1 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_USESSL", Descricao = "UseSsl", Valor = "true" };
            var parametro2 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_SERVERSMTP", Descricao = "Smtp server", Valor = "smtp.gmail.com" };
            var parametro3 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_SERVERPORT", Descricao = "Porta smtp", Valor = "587" };
            var parametro4 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_SENDER", Descricao = "Sender (email)", Valor = "contact@scrumtopractice.com" };
            var parametro5 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_SENDERPASSWORD", Descricao = "Password", Valor = "b8c7p2c6" };

            // Act
            int id1 = service.Gravar(parametro1);
            int id2 = service.Gravar(parametro2);
            int id3 = service.Gravar(parametro3);
            int id4 = service.Gravar(parametro4);
            int id5 = service.Gravar(parametro5);

            // Assert
            Assert.IsTrue(id1 > 0);
            Assert.IsTrue(id2 > 0);
            Assert.IsTrue(id3 > 0);
            Assert.IsTrue(id4 > 0);
            Assert.IsTrue(id5 > 0);
        }

        [TestMethod]
        public void ListarParametro()
        {
            // Arrange
            List<SistemaParametro> parametros;

            // Act
            parametros = service.Listar().ToList();

            // Assert
            Assert.AreNotEqual(0, parametros.Count);
        }
    }
}
