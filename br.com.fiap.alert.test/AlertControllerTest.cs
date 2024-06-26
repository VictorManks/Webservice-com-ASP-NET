using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using br.com.fiap.alert.api.Controllers;
using br.com.fiap.alert.api.Models;
using br.com.fiap.alert.api.Service;
using br.com.fiap.alert.api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace br.com.fiap.alert.test
{
    public class AlertControllerTest
    {
        private readonly Mock<IAlertService> _mockService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AlertController _alertController;

        public AlertControllerTest()
        {
            _mockService = new Mock<IAlertService>();
            _mockMapper = new Mock<IMapper>();
            _alertController = new AlertController(_mockService.Object, _mockMapper.Object);
        }

        [Fact]
        public void Get_ShouldReturnOkWithPaginatedAlerts()
        {
            // Arrange
            var referencia = 10;
            var tamanho = 5;
            var alerts = new List<AlertModel>
            {
                new AlertModel { AlertId = 11, TypeAlert = "Type 11", Message = "msg 11", Coords = "c11", Author = "A11" },
                new AlertModel { AlertId = 12, TypeAlert = "Type 12", Message = "msg 12", Coords = "c12", Author = "A12" },
                new AlertModel { AlertId = 13, TypeAlert = "Type 13", Message = "msg 13", Coords = "c13", Author = "A13" },
                new AlertModel { AlertId = 14, TypeAlert = "Type 14", Message = "msg 14", Coords = "c14", Author = "A14" },
                new AlertModel { AlertId = 15, TypeAlert = "Type 15", Message = "msg 15", Coords = "c15", Author = "A15" },
            };
            var viewModelList = alerts.Select(a => new AlertViewModel
            {
                AlertId = a.AlertId,
                TypeAlert = a.TypeAlert,
                Message = a.Message,
                Coords = a.Coords,
                Author = a.Author
            }).ToList();
            _mockService.Setup(s => s.ListarTodosAlertUltimaReferencia(referencia, tamanho)).Returns(alerts);
            _mockMapper.Setup(m => m.Map<IEnumerable<AlertViewModel>>(alerts)).Returns(viewModelList);

            // Act
            var result = _alertController.Get(referencia, tamanho);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var viewModel = Assert.IsType<AlertPaginacaoRefencialViewModel>(okResult.Value);
            Assert.Equal(viewModelList, viewModel.Alerts);
            Assert.Equal(tamanho, viewModel.PageSize);
            Assert.Equal(referencia, viewModel.Ref);
            Assert.Equal(viewModelList.Last().AlertId, viewModel.NextRef);
        }

        [Fact]
        public void Get_WithEmptyAlerts_ShouldReturnNoContent()
        {
            // Arrange
            var referencia = 10;
            var tamanho = 5;
            var alerts = new List<AlertModel>();
            _mockService.Setup(s => s.ListarTodosAlertUltimaReferencia(referencia, tamanho)).Returns(alerts);
            _mockMapper.Setup(m => m.Map<IEnumerable<AlertViewModel>>(alerts)).Returns(alerts.Select(a => new AlertViewModel
            {
                AlertId = a.AlertId,
                TypeAlert = a.TypeAlert,
                Message = a.Message,
                Coords = a.Coords,
                Author = a.Author
            }));

            // Act
            var result = _alertController.Get(referencia, tamanho);

            // Assert
            Assert.IsType<NoContentResult>(result.Result); 
        }
    }
}