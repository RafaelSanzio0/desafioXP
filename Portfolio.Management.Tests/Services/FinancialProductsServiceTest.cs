using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Portfolio.Management.Domain.Dtos.Request;
using Portfolio.Management.Domain.Dtos.Response;
using Portfolio.Management.Domain.Entities;
using Portfolio.Management.Domain.Enums;
using Portfolio.Management.Domain.Repositories;
using Portfolio.Management.Domain.Services.FinancialProduct;

namespace Portfolio.Management.Tests.Services
{
    public class FinancialProductServiceTests
    {
        private Mock<IDataReadRepository> _mockDataReadRepository;
        private Mock<IDataWritterRepository> _mockDataWritterRepository;
        private Mock<ILogger<FinancialProductService>> _mockLogger;
        private FinancialProductService _service;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _mockDataReadRepository = new();
            _mockDataWritterRepository = new();
            _mockLogger = new();
            _fixture = new();
            _service = new FinancialProductService(
                _mockLogger.Object,
                _mockDataReadRepository.Object,
                _mockDataWritterRepository.Object);
        }

        [Test]
        public async Task CreateFinancialProductAsync_ShouldReturnSuccessResponse_WhenProductIsCreated()
        {
            // Arrange
            var financialProductRequest = _fixture.Create<CreateFinancialProductRequest>();
            var financialProductEntity = _fixture.Create<FinancialProductEntity>();

            _mockDataWritterRepository.Setup(x => x.CreateFinancialProductAsync(financialProductRequest)).ReturnsAsync(financialProductEntity);

            // Act
            var result = await _service.CreateFinancialProductAsync(financialProductRequest);

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task DeleteFinancialProductAsync_ShouldReturnSuccessResponse_WhenProductIsDeleted()
        {
            // Arrange
            int id = 1;
            var entity = new FinancialProductEntity { Id = id };

            _mockDataReadRepository.Setup(x => x.GetFinancialProductsByIdAsync(id)).ReturnsAsync(entity);
            _mockDataWritterRepository.Setup(x => x.DeleteFinancialProductAsync(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteFinancialProductAsync(id);

            // Assert
            Assert.IsInstanceOf<SuccessResponse>(result.AsT0);
        }

        [Test]
        public async Task GetAllFinancialProductsAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            var entities = new List<FinancialProductEntity>
            {
                new() { Id = 1, Name = "Test Product 1", Type = FinancialProductType.Bond, Price = 100.00m, ExpirationDate = DateTime.Now.AddYears(1) },
                new() { Id = 2, Name = "Test Product 2", Type = FinancialProductType.Stock, Price = 200.00m, ExpirationDate = DateTime.Now.AddYears(2) }
            };

            _mockDataReadRepository.Setup(x => x.GetAllFinancialProductsAsync()).ReturnsAsync(entities);

            // Act
            var result = await _service.GetAllFinancialProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}
