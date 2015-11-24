using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Avanhandava.Domain.Service.Admin;
using Avanhandava.Domain.Models.Admin;

namespace Avanhandava.Tests.Admin
{
    [TestClass]
    public class UsuarioUnitTest
    {
        UsuarioService service;

        public UsuarioUnitTest()
        {
            service = new UsuarioService();
        }

        [TestMethod]
        public void IncluirUsuario()
        {
            // Arrange
            var usuario = new Usuario
            {
                Email = "jb.alessandro@gmail.com",
                Login = "jose",
                Nome = "jose alessandro",
                Ramal = "",
                Roles = "",
                Senha = "b8c7p2c6",
                Telefone = "997218670"
            };

            // Act
            usuario.Id = service.Gravar(usuario);

            // Assert
            Assert.IsTrue(usuario.Id > 0);
        }
    }
}
